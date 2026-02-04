using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Features.Authentication.Commands.RegisterUser;
using Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Features.Authentication.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password)
    : IRequest<LoginUserResult>;

public sealed record LoginUserResult(
    string AccessToken,
    string RefreshToken
);

public sealed class LoginUserHandler(
    IUsersRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenService tokenService)
    : IRequestHandler<LoginUserCommand, LoginUserResult>
{

    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email.ToLowerInvariant(), cancellationToken);

        if (user is null)
        {
            throw new NoEntityFoundException($"There is no user with email ${request.Email}");
        }

        var isPasswordValid =
            await passwordHasher.VerifyPasswordAsync(request.Password, user.PasswordHash!, cancellationToken);

        if (!isPasswordValid)
            throw new NoEntityFoundException("There is no user with such phoneNumber and password.");

        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();

        return new LoginUserResult(accessToken, refreshToken);
    }
}

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100);

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid role");
    }
}

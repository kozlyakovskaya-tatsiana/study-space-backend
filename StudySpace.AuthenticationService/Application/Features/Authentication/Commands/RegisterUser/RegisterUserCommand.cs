using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Validation;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.Authentication.Commands.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, RoleType Role) : IRequest<RegisterUserResult>;

public sealed record RegisterUserResult(Guid UserId);

public sealed class RegisterUserHandler(IUsersRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsAsync(u => u.Email == request.Email, cancellationToken))
            throw new InvalidOperationException("User already exists");

        var hash = await passwordHasher.HashPasswordAsync(request.Password, cancellationToken);

        var user = UserEntity.Create(request.Email, hash, [request.Role]);

        await userRepository.CreateAsync(user, cancellationToken);

        return new RegisterUserResult(user.Id);
    }
}

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).EmailRulesSet();
        RuleFor(x => x.Password).PasswordRulesSet();
        RuleFor(x => x.Role).IsInEnum();
    }
}
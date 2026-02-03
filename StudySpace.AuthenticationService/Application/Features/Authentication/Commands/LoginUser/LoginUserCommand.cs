using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;

namespace Application.Features.Authentication.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password)
    : IRequest<LoginUserResult>;

public sealed record LoginUserResult(
    string AccessToken,
    string RefreshToken,
    IReadOnlyCollection<RoleInfo> Roles
);

public sealed class LoginUserHandler(
    IUsersRepository userRepository,
    IPasswordHasher passwordHasher,
    IAuthService authService)
    : IRequestHandler<LoginUserCommand, LoginUserResult>
{

    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email.ToLowerInvariant(), cancellationToken);

        if (user is null || !await passwordHasher.VerifyPasswordAsync(request.Password, user.PasswordHash!, cancellationToken))
            throw new InvalidOperationException("Invalid credentials");

        var accessToken = authService.GenerateAccessToken(user);
        var refreshToken = authService.GenerateRefreshToken();

        var roles = user.Roles!
            .Select(r => RoleTypeMetadata.Get(r.Role))
            .ToList()
            .AsReadOnly();

        return new LoginUserResult(accessToken, refreshToken, roles);
    }
}

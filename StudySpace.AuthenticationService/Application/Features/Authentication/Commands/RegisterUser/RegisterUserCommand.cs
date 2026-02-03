using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authentication.Commands.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password) : IRequest<RegisterUserResult>;

public sealed record RegisterUserResult(Guid UserId);

public sealed class RegisterUserHandler(IUsersRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email;

        var exists = await userRepository.ExistsAsync(
            u => u.Email == email,
            cancellationToken);

        if (exists)
            throw new InvalidOperationException("User already exists");

        var hash = await passwordHasher.HashPasswordAsync(request.Password, cancellationToken);

        var user = UserEntity.Create(email, hash);

        await userRepository.CreateAsync(user, cancellationToken);

        return new RegisterUserResult(user.Id);
    }
}
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IAuthService
{
    string GenerateAccessToken(UserEntity user);
    string GenerateRefreshToken();
}
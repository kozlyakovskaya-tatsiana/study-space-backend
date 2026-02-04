using Domain.Entities;

namespace Application.Abstractions.Services;

public interface ITokenService
{
    string GenerateAccessToken(UserEntity user);
    string GenerateRefreshToken();
}
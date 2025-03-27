using iStoq.Application.DTOs;

namespace iStoq.Application.Interfaces;

public interface IAuthService
{
    LoginResponseDto? Authenticate(LoginRequestDto dto);
}
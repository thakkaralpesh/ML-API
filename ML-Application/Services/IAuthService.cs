using ML_Application.DTOs.Auth;

namespace ML_Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
        Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
        Task RequestPasswordResetAsync(RequestPasswordResetRequest request, CancellationToken ct = default);
        Task ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct = default);
    }
}

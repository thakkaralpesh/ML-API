namespace ML_Application.DTOs.Auth
{
    public class RegisterRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string MobileNumber { get; set; } = default!;
        public string? Email { get; set; }
        public string Password { get; set; } = default!;
    }

    public class LoginRequest
    {
        public string MobileOrEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class AuthResponse
    {
        public string AccessToken { get; set; } = default!;
        public string? RefreshToken { get; set; } // if you implement refresh
        public DateTime ExpiresAtUtc { get; set; }
        public string UserId { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }

    public class RequestPasswordResetRequest
    {
        public string MobileOrEmail { get; set; } = default!;
    }

    public class ResetPasswordRequest
    {
        public string Token { get; set; } = default!;  // OTP / reset token
        public string NewPassword { get; set; } = default!;
    }
}

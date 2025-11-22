using Microsoft.EntityFrameworkCore;
using ML_Application.DTOs.Auth;
using ML_Application.Services;
using ML_Domain.Entities;
using ML_Infrastructure.Persistence;

namespace ML_Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly MoneyLenderDbContext _dbContext;
        private readonly IJwtTokenGenerator _jwt;

        public AuthService(MoneyLenderDbContext dbContext, IJwtTokenGenerator jwt)
        {
            _dbContext = dbContext;
            _jwt = jwt;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
        {
            // Check existing user
            var exists = await _dbContext.Users
                .AnyAsync(u => u.MobileNumber == request.MobileNumber || u.Email == request.Email, ct);

            if (exists)
                throw new InvalidOperationException("User with this mobile or email already exists.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                MobileNumber = request.MobileNumber.Trim(),
                Email = request.Email?.Trim(),
                PasswordHash = passwordHash,
                Role = "Lender"
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(ct);

            var (token, expiresAt) = _jwt.GenerateToken(user);

            return new AuthResponse
            {
                AccessToken = token,
                ExpiresAtUtc = expiresAt,
                UserId = user.Id.ToString(),
                Role = user.Role,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var query = _dbContext.Users.AsQueryable();

            var user = await query.FirstOrDefaultAsync(
                u => u.MobileNumber == request.MobileOrEmail || u.Email == request.MobileOrEmail,
                ct
            );

            if (user is null || !user.IsActive)
                throw new UnauthorizedAccessException("Invalid credentials.");

            var validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!validPassword)
                throw new UnauthorizedAccessException("Invalid credentials.");

            var (token, expiresAt) = _jwt.GenerateToken(user);

            return new AuthResponse
            {
                AccessToken = token,
                ExpiresAtUtc = expiresAt,
                UserId = user.Id.ToString(),
                Role = user.Role,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }

        public async Task RequestPasswordResetAsync(RequestPasswordResetRequest request, CancellationToken ct = default)
        {
            // Simplified: in real app generate token, store, send SMS/email.
            var user = await _dbContext.Users.FirstOrDefaultAsync(
                u => u.MobileNumber == request.MobileOrEmail || u.Email == request.MobileOrEmail,
                ct
            );

            if (user is null)
            {
                // For security, don't reveal; just return.
                return;
            }

            // TODO: implement reset token generation + persistence + notification.
            await Task.CompletedTask;
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct = default)
        {
            // TODO: validate token, find user, etc.

            // Example pseudo-implementation:
            // var resetToken = await _dbContext.PasswordResetTokens
            //    .Include(t => t.User)
            //    .FirstOrDefaultAsync(t => t.Token == request.Token && !t.Used && t.ExpiresAtUtc > DateTime.UtcNow, ct);

            // For now, just throw NotImplemented:
            throw new NotImplementedException("Password reset via token not yet implemented.");
        }
    }
}

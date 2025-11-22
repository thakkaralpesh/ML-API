using Microsoft.EntityFrameworkCore;
using ML_Application.DTOs.Lenders;
using ML_Application.Services;
using ML_Domain.Entities;
using ML_Infrastructure.Persistence;

namespace ML_Infrastructure.Services
{
    public class LenderService : ILenderService
    {
        private readonly MoneyLenderDbContext _dbContext;

        public LenderService(MoneyLenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LenderResponse> CreateAsync(Guid userId, LenderCreateRequest request, CancellationToken ct = default)
        {
            // ensure user exists
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
            if (user is null)
                throw new InvalidOperationException("User not found");

            // prevent duplicate profile
            var existing = await _dbContext.Lenders.FirstOrDefaultAsync(l => l.UserId == userId, ct);
            if (existing is not null)
                throw new InvalidOperationException("Lender profile already exists.");

            var lender = new Lender
            {
                UserId = userId,
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                MobileNumber = request.MobileNumber.Trim(),
                BusinessName = request.BusinessName.Trim(),
                LicenseNumber = request.LicenseNumber?.Trim(),
                Jurisdiction = request.Jurisdiction.Trim(),
                BankName = request.BankName.Trim(),
                AccountNumber = request.AccountNumber.Trim(),
                IfscCode = request.IfscCode.Trim(),
                Branch = request.Branch?.Trim()
            };

            _dbContext.Lenders.Add(lender);
            await _dbContext.SaveChangesAsync(ct);

            return MapToResponse(lender);
        }

        public async Task<LenderResponse?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            var lender = await _dbContext.Lenders
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.UserId == userId, ct);

            return lender is null ? null : MapToResponse(lender);
        }

        public async Task<LenderResponse> UpdateAsync(Guid userId, LenderUpdateRequest request, CancellationToken ct = default)
        {
            var lender = await _dbContext.Lenders.FirstOrDefaultAsync(l => l.UserId == userId, ct);

            if (lender is null)
                throw new InvalidOperationException("Lender profile does not exist.");

            lender.FirstName = request.FirstName.Trim();
            lender.LastName = request.LastName.Trim();
            lender.MobileNumber = request.MobileNumber.Trim();
            lender.BusinessName = request.BusinessName.Trim();
            lender.LicenseNumber = request.LicenseNumber?.Trim();
            lender.Jurisdiction = request.Jurisdiction.Trim();
            lender.BankName = request.BankName.Trim();
            lender.AccountNumber = request.AccountNumber.Trim();
            lender.IfscCode = request.IfscCode.Trim();
            lender.Branch = request.Branch?.Trim();
            lender.UpdatedAtUtc = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(ct);

            return MapToResponse(lender);
        }

        private static LenderResponse MapToResponse(Lender lender) =>
            new()
            {
                Id = lender.Id,
                UserId = lender.UserId,
                FirstName = lender.FirstName,
                LastName = lender.LastName,
                MobileNumber = lender.MobileNumber,
                BusinessName = lender.BusinessName,
                LicenseNumber = lender.LicenseNumber,
                Jurisdiction = lender.Jurisdiction,
                BankName = lender.BankName,
                AccountNumber = lender.AccountNumber,
                IfscCode = lender.IfscCode,
                Branch = lender.Branch,
                CreatedAtUtc = lender.CreatedAtUtc,
                UpdatedAtUtc = lender.UpdatedAtUtc
            };
    }
}

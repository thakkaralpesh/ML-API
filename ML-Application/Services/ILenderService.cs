using ML_Application.DTOs.Lenders;

namespace ML_Application.Services
{
    public interface ILenderService
    {
        Task<LenderResponse> CreateAsync(Guid userId, LenderCreateRequest request, CancellationToken ct = default);
        Task<LenderResponse?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        Task<LenderResponse> UpdateAsync(Guid userId, LenderUpdateRequest request, CancellationToken ct = default);
    }
}

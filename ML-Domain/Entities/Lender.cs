namespace ML_Domain.Entities
{
    public class Lender
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Link to User
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string MobileNumber { get; set; } = default!;
        public string BusinessName { get; set; } = default!;
        public string? LicenseNumber { get; set; }
        public string Jurisdiction { get; set; } = default!;

        // Bank details
        public string BankName { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string IfscCode { get; set; } = default!;
        public string? Branch { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }
    }
}

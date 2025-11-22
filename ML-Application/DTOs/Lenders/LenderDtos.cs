namespace ML_Application.DTOs.Lenders
{
    public class LenderCreateRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string MobileNumber { get; set; } = default!;
        public string BusinessName { get; set; } = default!;
        public string? LicenseNumber { get; set; }
        public string Jurisdiction { get; set; } = default!;

        public string BankName { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string IfscCode { get; set; } = default!;
        public string? Branch { get; set; }
    }

    public class LenderUpdateRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string MobileNumber { get; set; } = default!;
        public string BusinessName { get; set; } = default!;
        public string? LicenseNumber { get; set; }
        public string Jurisdiction { get; set; } = default!;

        public string BankName { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string IfscCode { get; set; } = default!;
        public string? Branch { get; set; }
    }

    public class LenderResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string MobileNumber { get; set; } = default!;
        public string BusinessName { get; set; } = default!;
        public string? LicenseNumber { get; set; }
        public string Jurisdiction { get; set; } = default!;

        public string BankName { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string IfscCode { get; set; } = default!;
        public string? Branch { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}

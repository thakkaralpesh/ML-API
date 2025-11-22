namespace ML_Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string MobileNumber { get; set; } = default!; // unique
        public string Email { get; set; } = default!;        // optional or unique
        public string PasswordHash { get; set; } = default!;

        public string Role { get; set; } = "Lender"; // "Admin", "Lender"
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}

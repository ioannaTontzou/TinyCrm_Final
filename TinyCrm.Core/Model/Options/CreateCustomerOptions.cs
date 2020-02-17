namespace TinyCrm.Core.Model.Options
{
    public class CreateCustomerOptions
    {
        public string VatNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool isActive { get; set; }
        public string CountryCode { get; set; }
    }
}

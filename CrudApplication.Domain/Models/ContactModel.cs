namespace CrudApplication.Models
{
    public class ContactModel:BaseModel
    {
        public string? ContactName { get; set; }

        public CompanyModel Company { get; set; }
        public CountryModel Country { get; set; }
    }
}

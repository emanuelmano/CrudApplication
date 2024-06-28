using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApplication.Models.Entities
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        public required string ContactName { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }

        public Company? Company { get; set; }
        public Country? Country { get; set; }

    }
}

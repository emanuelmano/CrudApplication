using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApplication.Models.Entities
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public required string CompanyName { get; set; }

        [JsonIgnore]
        public  ICollection<Contact>? Contacts { get; set; } 
    }
}

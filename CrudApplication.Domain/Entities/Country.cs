using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApplication.Models.Entities
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        [JsonIgnore]
        public ICollection<Contact>? Contacts { get; set; }
    }
}

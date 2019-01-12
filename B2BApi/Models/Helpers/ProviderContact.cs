using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;

namespace B2BApi.Models.Helpers
{
    public class ProviderContact
    {
        [Key]
        public int Id { get; set; }
        public ProviderType ProviderType { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
    }
}
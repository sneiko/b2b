using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;
using Newtonsoft.Json;

namespace B2BApi.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        [JsonIgnore]
        public virtual Credentials Credentials { get; set; }
        [JsonIgnore]
        public virtual CompleteToken Token { get; set; }
    }
}
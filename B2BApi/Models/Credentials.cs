using Newtonsoft.Json;

namespace B2BApi.Models
{
    public class Credentials
    {
        public string Password { get; set; }

        public string Salt { get; set; }

        #region Navigation properties

        [JsonIgnore]
        public int UserId { get; set; }
        
        [JsonIgnore]
        public virtual User User { get; set; }

        #endregion    
    }
}
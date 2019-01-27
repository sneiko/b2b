using System;
using Newtonsoft.Json;

namespace B2BApi.Models
{
    public class CompleteToken
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpirationTime { get; set; }
        
        #region Navigation properties

        [JsonIgnore]
        public int UserId { get; set; }
        
        [JsonIgnore]
        public virtual User User { get; set; }

        #endregion
    }
}
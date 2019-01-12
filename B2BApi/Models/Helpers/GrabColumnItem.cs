using System.ComponentModel.DataAnnotations;
using B2BApi.Models.Enum;

namespace B2BApi.Models.Helpers
{
    public class GrabColumnItem
    {
        [Key]
        public int Id { get; set; }
        public GrabColumn GrabColumn { get; set; }
        public byte Value { get; set; }
    }
}
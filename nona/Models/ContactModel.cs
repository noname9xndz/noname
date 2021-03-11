using System.ComponentModel.DataAnnotations;

namespace nona.Models
{
    public class ContactModel
    {
        [Required]
        public string Name { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Subject { set; get; }
        [Required]
        public string Message { set; get; }
    }
}

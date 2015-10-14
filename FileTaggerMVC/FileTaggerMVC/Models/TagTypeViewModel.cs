using System.ComponentModel.DataAnnotations;

namespace FileTaggerMVC.Models
{
    public class TagTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
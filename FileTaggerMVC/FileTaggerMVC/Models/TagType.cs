using System.ComponentModel.DataAnnotations;

namespace FileTaggerMVC.Models
{
    public class TagType
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
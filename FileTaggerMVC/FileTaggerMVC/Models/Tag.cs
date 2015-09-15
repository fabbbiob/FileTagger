using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileTaggerMVC.Models
{
    [Table("Tag")]
    public class Tag
    {
        [Column, Key]
        public int Id { get; set; }
        [Column, MaxLength(255)]
        public string Description { get; set; }
        public TagType TagType { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileTaggerMVC.Models
{
    [Table("TagType")]
    public class TagType
    {
        [Column, Key]
        public int Id { get; set; }
        [Column, MaxLength(255)]
        public string Description { get; set; }
    }
}
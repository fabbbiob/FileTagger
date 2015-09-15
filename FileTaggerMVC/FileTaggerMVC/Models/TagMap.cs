using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileTaggerMVC.Models
{
    [Table("TagMap")]
    public class TagMap
    {
        [Column, Key]
        public int Id { get; set; }
        [Column, ForeignKey("FileId")]
        public File File { get; set; }
        [Column, ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
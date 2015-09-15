using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileTaggerMVC.Models
{
    [Table("File")]
    public class File
    {
        [Column, Key]
        public int Id { get; set; }
        [Column, MaxLength(255)]
        public string FilePath { get; set; }
    }
}
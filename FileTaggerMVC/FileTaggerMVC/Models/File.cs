using System.Collections.Generic;

namespace FileTaggerMVC.Models
{
    public class File
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
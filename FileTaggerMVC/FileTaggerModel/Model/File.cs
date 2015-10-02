using System.Collections.Generic;

namespace FileTaggerModel.Models
{
    public class File
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}

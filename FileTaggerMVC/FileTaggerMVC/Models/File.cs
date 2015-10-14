using System.Collections.Generic;

namespace FileTaggerMVC.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
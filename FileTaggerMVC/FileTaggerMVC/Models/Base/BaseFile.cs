using System.Collections.Generic;

namespace FileTaggerMVC.Models.Base
{
    public class BaseFile
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public List<BaseTag> Tags { get; set; }
    }
}
using System.Web.Mvc;

namespace FileTaggerMVC.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string FileName => FilePath.Substring(FilePath.LastIndexOf(@"\") + 1);

        public int[] TagIds { get; set; }
        public MultiSelectList Tags { get; set; }
    }
}
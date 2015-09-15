namespace FileTaggerMVC.Models
{
    public class TagMap
    {
        public int Id { get; set; }
        public File File { get; set; }
        public Tag Tag { get; set; }
    }
}
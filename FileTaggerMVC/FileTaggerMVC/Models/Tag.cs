namespace FileTaggerMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TagType TagType { get; set; }
    }
}
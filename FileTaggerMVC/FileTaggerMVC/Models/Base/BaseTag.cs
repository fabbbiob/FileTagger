namespace FileTaggerMVC.Models.Base
{
    public class BaseTag
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public BaseTagType TagType { get; set; }
    }
}
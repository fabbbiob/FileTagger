using System.Collections.Generic;

namespace FileTaggerModel.Models
{
    public class TagType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<Tag> Tags;
    }
}

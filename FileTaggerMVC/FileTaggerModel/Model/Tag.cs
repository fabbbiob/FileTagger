using System.Collections.Generic;

namespace FileTaggerModel.Model
{
    public class Tag : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TagType TagType { get; set; }
        public IEnumerable<File> Files;
    }
}

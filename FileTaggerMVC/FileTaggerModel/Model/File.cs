using System.Collections.Generic;

namespace FileTaggerModel.Model
{
    public class File : IEntity
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}

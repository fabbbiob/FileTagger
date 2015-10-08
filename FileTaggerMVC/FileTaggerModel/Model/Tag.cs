using System.Collections.Generic;

namespace FileTaggerModel.Model
{
    public class Tag : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TagType TagType { get; set; }
        public IEnumerable<File> Files;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Tag other = (Tag)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

using System.Collections.Generic;

namespace FileTaggerModel.Model
{
    public class TagType : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<Tag> Tags;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            { 
                return false;
            }

            TagType other = (TagType)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

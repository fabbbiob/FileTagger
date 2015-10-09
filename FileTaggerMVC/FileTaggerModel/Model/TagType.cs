using System.Collections.Generic;
using System.Linq;

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
            if (Id != other.Id || Description != other.Description)
            {
                return false;
            }

            if ((Tags != null && other.Tags == null) || (Tags == null && other.Tags != null))
            {
                return false;
            }

            if(Tags != null && other.Tags != null)
            { 
                foreach (var elem in Tags.Zip(other.Tags, (a, b) => new { First = a, Second = b }))
                {
                    if (!elem.First.Equals(elem.Second))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

using System.Collections.Generic;

namespace FileTaggerModel.Model
{
    public class File : IEntity
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public List<Tag> Tags { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            File other = (File)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

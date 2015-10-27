using FileTaggerModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTaggerRepository.Repositories.Abstract
{
    interface ITagRepository
    {
        int Add(Tag tagType);
        void Update(Tag tagType);
        void Delete(int id);
        IEnumerable<Tag> GetAll();
        Tag GetById(int id);
    }
}

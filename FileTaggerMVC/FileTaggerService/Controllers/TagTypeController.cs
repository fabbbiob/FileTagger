using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerRepository.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class TagTypeController : ApiController
    {
        private readonly ITagTypeRepository _tagTypeRepository;

        public TagTypeController() : base()
        {
            _tagTypeRepository = new TagTypeRepository();
        }

        public void Add(TagType tagType)
        {
            _tagTypeRepository.Add(tagType);
        }

        public void Update(TagType tagType)
        {
            _tagTypeRepository.Update(tagType);
        }

        public void Delete(int id)
        {
            _tagTypeRepository.Delete(id);
        }

        public IEnumerable<TagType> Get()
        {
            return _tagTypeRepository.GetAll();
        }

        public TagType Get(int id)
        {
            return _tagTypeRepository.GetById(id);
        }
    }
}

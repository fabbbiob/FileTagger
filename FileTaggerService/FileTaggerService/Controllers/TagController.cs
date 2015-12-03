using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using System.Collections.Generic;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class TagController : ApiController
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository) : base()
        {
            _tagRepository = tagRepository;
        }

        // GET: api/Tag
        public IEnumerable<Tag> Get()
        {
            return _tagRepository.GetAll();
        }

        // GET: api/Tag/5
        public Tag Get(int id)
        {
            return _tagRepository.GetById(id);
        }

        // POST: api/Tag
        public void Post([FromBody]Tag tag)
        {
            _tagRepository.Add(tag);
        }

        // PUT: api/Tag
        public void Put([FromBody]Tag tag)
        {
            _tagRepository.Update(tag);
        }

        // DELETE: api/Tag/5
        public void Delete(int id)
        {
            _tagRepository.Delete(id);
        }
    }
}

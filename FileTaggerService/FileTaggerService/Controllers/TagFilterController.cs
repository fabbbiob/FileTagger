using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using System.Collections.Generic;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class TagFilterController : ApiController
    {
        private readonly ITagRepository _tagRepository;

        public TagFilterController(ITagRepository tagRepository) : base()
        {
            _tagRepository = tagRepository;
        }

        // GET: api/TagFilter?tagIds=5
        public IEnumerable<Tag> Get([FromUri]int[] tagIds)
        {
            if (tagIds == null || tagIds.Length == 0)
            {
                return _tagRepository.GetAll();
            }
            else
            {
               return _tagRepository.FilteredTags(tagIds);
            }
        }

    }
}

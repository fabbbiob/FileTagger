using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Impl.Simple;
using FileTaggerRepository.Repositories.Impl.Complex;
using NUnit.Framework;

namespace FileTaggerRepositoryTests.Tests
{
    [TestFixture]
    public class TagTypeWithTagsRepositoryTest
    {
        [Test]
        public void CanGetTagTypeWithTags()
        {
            TagTypeRepository tagTypeRepository = new TagTypeRepository();
            TagType tagType = new TagType
            {
                Description = "my tag type"
            };
            tagTypeRepository.Add(tagType);

            TagRepository tagRepository = new TagRepository();
            Tag tag1 = new Tag
            {
                Description = "tag1",
                TagType = tagType
            };
            tagRepository.Add(tag1);

            Tag tag2 = new Tag
            {
                Description = "tag2",
                TagType = tagType
            };
            tagRepository.Add(tag2);

            tagType.Tags = new[] {tag1, tag2};

            TagTypeWithTagsRepository tagTypeWithTagsRepository = new TagTypeWithTagsRepository();
            Assert.AreEqual(tagType, tagTypeWithTagsRepository.GetById(tagType.Id));
        }
    }
}

using System.Data.SQLite;
using System.Linq;
using FileTaggerModel.Model;
using FileTaggerRepository.Repositories;
using FileTaggerRepository.Repositories.Impl;
using NUnit.Framework;

namespace FileTaggerRepositoryTests.Tests
{
    [TestFixture]
    public class TagRepositoryTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            DbCreator.CreateDatabase();
        }

        [Test]
        public void CanAddTagType()
        {
            TagRepository repo = new TagRepository();
            repo.Add(new Tag
            {
                Description = "tag"
            });
        }

        [Test, ExpectedException(typeof(SQLiteException))]
        public void CanFailWithNullDescriptionAndNullTagType()
        {
            TagRepository repo = new TagRepository();
            repo.Add(new Tag());
        }

        [Test]
        public void CanGetTag()
        {
            TagRepository repo = new TagRepository();
            Tag tag = new Tag
            {
                Description = "tag"
            };
            repo.Add(tag);

            Assert.AreEqual(tag, repo.GetById(tag.Id));
        }

        [Test]
        public void CanUpdateTag()
        {
            TagTypeRepository repoTagtypes = new TagTypeRepository();
            TagType tagTypeOriginal = new TagType
            {
                Description = "tag type original"
            };
            TagType tagTypeUpdated = new TagType
            {
                Description = "tag type updated"
            };
            repoTagtypes.Add(tagTypeOriginal);
            repoTagtypes.Add(tagTypeUpdated);

            TagRepository repoTags = new TagRepository();
            Tag tag = new Tag
            {
                Description = "tag",
                TagType = tagTypeOriginal
            };
            repoTags.Add(tag);

            tag.Description = "tag updated";
            tag.TagType = tagTypeUpdated;
            repoTags.Update(tag);

            Assert.AreEqual(tag, repoTags.GetById(tag.Id));
        }

        [Test]
        public void CanDeleteTag()
        {
            TagRepository repo = new TagRepository();
            Tag tag = new Tag
            {
                Description = "tag"
            };
            repo.Add(tag);

            repo.Delete(tag);

            Assert.Null(repo.GetById(tag.Id));
        }

        [Test]
        public void CanGetAllTags()
        {
            TagRepository repo = new TagRepository();
            Tag tag1 = new Tag { Description = "tag1" };
            Tag tag2 = new Tag { Description = "tag2" };
            repo.Add(tag1);
            repo.Add(tag2);

            Tag[] tags = repo.GetAll().ToArray();

            Assert.True(tags.Contains(tag1));
            Assert.True(tags.Contains(tag2));
        }
    }
}

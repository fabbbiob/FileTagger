using System.Data.SQLite;
using FileTaggerModel.Model;
using FileTaggerRepository.Repositories;
using FileTaggerRepository.Repositories.Impl.Simple;
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
    }
}

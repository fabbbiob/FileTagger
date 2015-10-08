using FileTaggerRepository.Repositories;
using NUnit.Framework;

namespace FileTaggerRepositoryTests.Tests
{
    [TestFixture]
    public class DbCreatorTest
    {
        [Test]
        public void CreateDatabase()
        {
            DbCreator.CreateDatabase();
        }
    }
}

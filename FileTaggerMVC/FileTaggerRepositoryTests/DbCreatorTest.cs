using FileTaggerRepository.Repositories;
using NUnit.Framework;

namespace FileTaggerRepositoryTests
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

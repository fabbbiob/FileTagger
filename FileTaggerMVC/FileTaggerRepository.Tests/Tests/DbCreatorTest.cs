using FileTaggerRepository.Helpers;
using NUnit.Framework;

namespace FileTaggerRepositoryTests.Tests
{
    [TestFixture]
    public class DbCreatorTest
    {
        [Test]
        public void CreateDatabase()
        {
            //DbCreator.DeleteDatabase();
            DbCreator.CreateDatabase();
        }
    }
}

using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerService.Controllers;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace FileTaggerService.Tests.Tests
{
    [TestFixture]
    public class TagControllerTest
    {
        [Test]
        public void CanGetTagType()
        {
            Tag expected = new Tag { Id = 1, Description = "abc" };
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            mock.Setup(f => f.GetById(1))
                .Returns(expected);

            TagController controller = new TagController(mock.Object);
            Tag tag = controller.Get(1);

            Assert.AreEqual(expected, tag);
        }



    }
}

using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerService.Controllers;
using Moq;
using NUnit.Framework;

namespace FileTaggerService.Tests
{
    [TestFixture]
    public class TagTypeControllerTest
    {
        [Test]
        public void CanGetTagType()
        {
            TagType expected = new TagType { Id = 1, Description = "abc" };
            Mock<ITagTypeRepository> mock = new Mock<ITagTypeRepository>();
            mock.Setup(f => f.GetById(1))
                .Returns(expected);

            TagTypeController controller = new TagTypeController(mock.Object);
            TagType tagType = controller.Get(1);

            Assert.AreEqual(expected, tagType);
        }
    }
}

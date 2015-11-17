using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerService.Controllers;
using Moq;
using NUnit.Framework;
using System.Linq;

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

        [Test]
        public void CanGetAllTagTypes()
        {
            TagType[] expected = new[] {
                new TagType { Id = 1, Description = "abc" },
                new TagType { Id = 2, Description = "def" }
            };
            Mock<ITagTypeRepository> mock = new Mock<ITagTypeRepository>();
            mock.Setup(f => f.GetAll())
                .Returns(expected);

            TagTypeController controller = new TagTypeController(mock.Object);
            TagType[] tagTypes = controller.Get().ToArray();

            Assert.AreEqual(expected, tagTypes);
        }

        [Test]
        public void CanFailGetTagType()
        {
            TagType expected = new TagType { Id = 1, Description = "abc" };
            Mock<ITagTypeRepository> mock = new Mock<ITagTypeRepository>();
            mock.Setup(f => f.GetById(1))
                .Returns(expected);

            TagTypeController controller = new TagTypeController(mock.Object);

            TagType tagType = controller.Get(2);

            Assert.AreNotEqual(expected, tagType);
        }

        [Test]
        public void CanAddTagType()
        {
            Mock<ITagTypeRepository> mock = new Mock<ITagTypeRepository>();
            TagTypeController controller = new TagTypeController(mock.Object);

            controller.Post(new TagType { Id = 1, Description = "abc" });
        }

        [Test]
        public void CanUpdateTagType()
        {
            Mock<ITagTypeRepository> mock = new Mock<ITagTypeRepository>();
            TagTypeController controller = new TagTypeController(mock.Object);

            controller.Put(new TagType { Id = 1, Description = "abc" });
        }

        [Test]
        public void CanDeleteUpdateTagType()
        {
            Mock<ITagTypeRepository> mock = new Mock<ITagTypeRepository>();
            TagTypeController controller = new TagTypeController(mock.Object);

            controller.Delete(1);
        }

    }
}

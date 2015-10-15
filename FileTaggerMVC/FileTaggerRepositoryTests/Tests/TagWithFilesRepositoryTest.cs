using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Impl;
using NUnit.Framework;

namespace FileTaggerRepositoryTests.Tests
{
    [TestFixture]
    public class TagWithFilesRepositoryTest
    {
        [Test]
        public void CanGetTagWithFiles()
        {
            TagRepository tagRepository = new TagRepository();
            Tag tag = new Tag
            {
                Description = "tag"
            };
            tagRepository.Add(tag);

            FileRepository fileRepository = new FileRepository();
            File file1 = new File
            {
                FilePath = Guid.NewGuid().ToString(),
                Tags = new[] { tag }
            };
            fileRepository.Add(file1);

            File file2 = new File
            {
                FilePath = Guid.NewGuid().ToString(),
                Tags = new[] { tag }
            };
            fileRepository.Add(file2);

            tag.Files = new[] { file1, file2 };

            Assert.AreEqual(tag, tagRepository.GetByIdWithReferences(tag.Id));
        }
    }
}

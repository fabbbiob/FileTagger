using System;
using System.Data.SQLite;
using System.Linq;
using FileTaggerModel.Model;
using FileTaggerRepository.Helpers;
using FileTaggerRepository.Repositories.Impl;
using NUnit.Framework;
using System.Collections.Generic;

namespace FileTaggerRepositoryTests.Tests
{
    [TestFixture]
    public class FileRepositoryTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            //DbCreator.DeleteDatabase();
            DbCreator.CreateDatabase();
        }

        [Test]
        public void CanAddFile()
        {
            FileRepository repo = new FileRepository();
            File file = new File
            {
                FilePath = Guid.NewGuid().ToString(),
                Tags = new List<Tag>()
            };

            repo.Add(file);

            Assert.AreNotEqual(0, file.Id);
        }

        [Test, ExpectedException(typeof(SQLiteException))]
        public void CanFailSamePath()
        {
            FileRepository repo = new FileRepository();
            const string filePath = "1";
            repo.Add(new File
            {
                FilePath = filePath,
                Tags = new List<Tag>()
            });
            repo.Add(new File
            {
                FilePath = filePath,
                Tags = new List<Tag>()
            });
        }

        [Test, ExpectedException(typeof(SQLiteException))]
        public void CanFailWithNullFilepath()
        {
            FileRepository repo = new FileRepository();
            repo.Add(new File { Tags = new List<Tag>() });
        }

        [Test]
        public void CanGetFile()
        {
            FileRepository repo = new FileRepository();
            File file = new File
            {
                FilePath = Guid.NewGuid().ToString(),
                Tags = new List<Tag>()
            };
            repo.Add(file);
            
            Assert.AreEqual(file, repo.GetByFilename(file.FilePath));
        }

        //[Test]
        //public void CanUpdateFile()
        //{
        //    FileRepository repo = new FileRepository();
        //    File file = new File
        //    {
        //        FilePath = Guid.NewGuid().ToString()
        //    };
        //    repo.Add(file);

        //    file.FilePath = Guid.NewGuid().ToString();
        //    repo.Update(file);

        //    Assert.AreEqual(file, repo.GetById(file.Id));
        //}

        //[Test]
        //public void CanDeleteFile()
        //{
        //    FileRepository repo = new FileRepository();
        //    File file = new File
        //    {
        //        FilePath = Guid.NewGuid().ToString()
        //    };
        //    repo.Add(file);

        //    repo.Delete(file);

        //    Assert.Null(repo.GetById(file.Id));
        //}

        //[Test]
        //public void CanGetAllFiles()
        //{
        //    FileRepository repo = new FileRepository();
        //    File file1 = new File { FilePath = Guid.NewGuid().ToString() };
        //    File file2 = new File { FilePath = Guid.NewGuid().ToString() };
        //    repo.Add(file1);
        //    repo.Add(file2);

        //    File[] files = repo.GetAll().ToArray();

        //    Assert.True(files.Contains(file1));
        //    Assert.True(files.Contains(file2));
        //}

        [Test]
        public void CanAddFileWithTags()
        {
            TagRepository tagRepository = new TagRepository();
            Tag tag1 = new Tag { Description = Guid.NewGuid().ToString() };
            tagRepository.Add(tag1);
            Tag tag2 = new Tag { Description = Guid.NewGuid().ToString() };
            tagRepository.Add(tag2);

            FileRepository fileRepository = new FileRepository();
            File file = new File
            {
                FilePath = Guid.NewGuid().ToString(),
                Tags = new [] { tag1, tag2 }
            };
            fileRepository.Add(file);

            Assert.AreEqual(file, fileRepository.GetByFilename(file.FilePath));
        }

        [Test]
        public void CanGetFilesByTag()
        {
            TagRepository tagRepository = new TagRepository();
            Tag tag = new Tag
            {
                Description = "CanGetFilesByTag tag"
            };
            tagRepository.Add(tag);

            FileRepository fileRepository = new FileRepository();
            File fileWithTag = new File
            {
                FilePath = Guid.NewGuid().ToString(),
                Tags = new []{ tag }
            };

            File fileWithoutTag = new File
            {
                FilePath = Guid.NewGuid().ToString(),
                Tags = new Tag[0]
            };

            fileRepository.Add(fileWithTag);
            fileRepository.Add(fileWithoutTag);

            IEnumerable<File> files = fileRepository.GetByTag(tag.Id);
            Assert.Contains(fileWithTag, files.ToArray());
        }
    }
}

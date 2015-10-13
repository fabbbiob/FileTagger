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
    //[TestFixture]
    public class TagWithFilesRepositoryTest
    {
        //[Test]
        public void CanGetTagWithFiles()
        {
            FileRepository fileRepository = new FileRepository();
            File file1 = new File
            {
                FilePath = "file1"
            };
            fileRepository.Add(file1);

            File file2 = new File
            {
                FilePath = "file2"
            };
            fileRepository.Add(file2);

            //TODO
        }
    }
}

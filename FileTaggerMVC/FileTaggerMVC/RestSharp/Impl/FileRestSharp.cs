using FileTaggerMVC.RestSharp.Abstract;
using System;
using System.Collections.Generic;
using FileTaggerModel.Model;
using RestSharp;

namespace FileTaggerMVC.RestSharp.Impl
{
    public class FileRestSharp : BaseRestSharp, IFileRest
    {
        public FileRestSharp() : base()
        {

        }

        public File Get(string fileName)
        {
            RestRequest request = new RestRequest("api/FileName", Method.GET);
            request.AddParameter("filename", fileName);
            IRestResponse<File> response = _client.Execute<File>(request);
            return response.Data;
        }

        public void Post(File file)
        {
            RestRequest request = new RestRequest("api/file", Method.POST);
            PostOrPut(request, file);
        }

        public void Put(File file)
        {
            RestRequest request = new RestRequest("api/file", Method.PUT);
            PostOrPut(request, file);
        }
    }
}
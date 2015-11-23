using FileTaggerMVC.RestSharp.Abstract;
using System;
using System.Collections.Generic;
using RestSharp;
using FileTaggerMVC.Models.Base;

namespace FileTaggerMVC.RestSharp.Impl
{
    public class FileRestSharp : BaseRestSharp, IFileRest
    {
        public FileRestSharp() : base()
        {

        }

        public BaseFile Get(string fileName)
        {
            RestRequest request = new RestRequest("api/FileName", Method.GET);
            request.AddParameter("filename", fileName);
            IRestResponse<BaseFile> response = _client.Execute<BaseFile>(request);
            return response.Data;
        }

        public void Post(BaseFile file)
        {
            RestRequest request = new RestRequest("api/file", Method.POST);
            PostOrPut(request, file);
        }

        public void Put(BaseFile file)
        {
            RestRequest request = new RestRequest("api/file", Method.PUT);
            PostOrPut(request, file);
        }
    }
}
using FilesUpload.Application.Contracts;
using FilesUpload.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Files.Upload.Upload.Repositories
{
    public class UploadFileRepository : IFileUpload
    {
        public Task<FileToUpload> GetFile(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<FileToUpload> UploadFile(IFormCollection formCollection)
        {
            try
            {
                var file = formCollection.Files.First();
                if (file == null && file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileType = ContentDispositionHeaderValue.Parse(file.ContentDisposition).DispositionType;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}

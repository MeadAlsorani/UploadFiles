using FilesUpload.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUpload.Application.Contracts
{
    public interface IFileUpload
    {
        Task<FileToUpload> GetFile(int Id);
        Task<FileToUpload> UploadFile(IFormCollection formCollection);
    }
}

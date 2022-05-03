using FilesUpload.Application.DTOs;
using FilesUpload.Application.Responses;
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
        Task<FileUplaodDto> GetFile(int Id);
        Task<BaseResponse<FileUplaodDto>> UploadFile(IFormCollection formCollection);
    }
}

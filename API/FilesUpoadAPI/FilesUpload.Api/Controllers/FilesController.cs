using Files.Upload.Upload.Repositories;
using FilesUpload.Application.Contracts;
using FilesUpload.Application.DTOs;
using FilesUpload.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilesUpload.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public IFileUpload FileRepository { get; }
        public FilesController(IFileUpload fileRepository)
        {
            FileRepository = fileRepository;
        }


        [HttpPost("UploadFile")]
        public async Task<ActionResult<FileUplaodDto>> UploadFile()
        {
            BaseResponse<FileUplaodDto> response;
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var uploadResult = await FileRepository.UploadFile(formCollection);
                return Ok(uploadResult);
            }
            catch (Exception ex)
            {
                return BadRequest(response = new BaseResponse<FileUplaodDto>(new List<string> { ex.Message }));
            }
        }
    }
}

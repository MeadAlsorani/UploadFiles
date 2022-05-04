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

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<FileUplaodDto>>> GetFile(int id)
        {
            var file = await FileRepository.GetFile(id);
            return HandleResponse(file);
        }

        [HttpGet("path/{id}")]
        public async Task<ActionResult<string>> GetFilePath(int id)
        {
            var path = await FileRepository.GetFilePath(id);
            return Ok(new { fullPath = path });
        }
        [HttpPost("UploadFile")]
        public async Task<ActionResult<BaseResponse<FileUplaodDto>>> UploadFile()
        {
            BaseResponse<FileUplaodDto> response;
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var uploadResult = await FileRepository.UploadFile(formCollection);
                return HandleResponse(uploadResult);
            }
            catch (Exception ex)
            {
                return BadRequest(response = new BaseResponse<FileUplaodDto>(new List<string> { ex.Message }));
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> DeleteFile(int id)
        {
            var deleteFile = await FileRepository.DeleteFile(id);
            return HandleResponse(deleteFile);
        }
        private ActionResult HandleResponse<T>(BaseResponse<T> response)
        {
            if (response.Data == null)
                return BadRequest(response);
            else
                return Ok(response);
        }
    }
}

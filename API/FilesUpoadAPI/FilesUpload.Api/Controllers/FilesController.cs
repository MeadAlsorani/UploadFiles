using Files.Upload.Upload.Repositories;
using FilesUpload.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilesUpload.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public UploadFileRepository FileRepository { get; }
        public FilesController(UploadFileRepository fileRepository)
        {
            FileRepository = fileRepository;
        }


        [HttpPost("UploadFile")]
        public async Task<ActionResult<FileUplaodDto>> UploadFile()
        {
            var formCollection = await Request.ReadFormAsync();
            var uploadResult = FileRepository.UploadFile(formCollection);
            return Ok(true);
        }
    }
}

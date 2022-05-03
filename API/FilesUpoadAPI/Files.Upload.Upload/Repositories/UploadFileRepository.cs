using AutoMapper;
using FilesUpload.Application.Contracts;
using FilesUpload.Application.DTOs;
using FilesUpload.Application.Responses;
using FilesUpload.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Files.Upload.Upload.Repositories
{
    public class UploadFileRepository : IFileUpload
    {
        private readonly IConfiguration _configuration;

        public FilesDbContext DbContext { get; }
        public IMapper _mapper { get; }

        public UploadFileRepository(FilesDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            DbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }


        public Task<FileUplaodDto> GetFile(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<FileUplaodDto>> UploadFile(IFormCollection formCollection)
        {
            BaseResponse<FileUplaodDto> response;
            try
            {
                var file = formCollection.Files.First();
                if (file != null && file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string contentType = file.ContentType;
                    string fileExtenstion = Path.GetExtension(fileName);
                    string folderName;
                    string typeName;
                    if (contentType == "application/pdf")
                    {
                        typeName = "PDF";
                    }
                    else if (contentType.StartsWith("image"))
                    {
                        typeName = "Image";
                    }
                    // if the file was a Word document
                    else if (contentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        typeName = "Word";
                    }
                    // if the file was an Excel document
                    else if (contentType == "application/vnd.ms-excel")
                    {
                        typeName = "Excel";
                    }
                    // if the file was none of above types
                    else
                    {
                        typeName = "Other";
                    }

                    folderName = Path.Combine("Resources", "Files", typeName);

                    string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    string guidFileName = $"{Guid.NewGuid()}-{fileName}";
                    string fullPath = Path.Combine(pathToSave, guidFileName);
                    string dbPath = Path.Combine(folderName, guidFileName);

                    var fileConfigs = _configuration.GetSection("FileConfigs");
                    var maxFileSize = fileConfigs["MaxSizeByKiloBytes"];
                    if (((double)file.Length / 1024) > Convert.ToInt32(maxFileSize))
                    {
                        response = new BaseResponse<FileUplaodDto>(new List<string> { "File size is bigger than permited limit" });
                        return response;
                    }
                    var allowedTypes = fileConfigs["AllowedFileTypes"];
                    var types = allowedTypes.Split('\u002C');
                    if (!types.Contains(fileExtenstion.Substring(1)))
                    {
                        response = new BaseResponse<FileUplaodDto>(new List<string> { "File type is not permited" });
                        return null;
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var result = await SaveFile(dbPath, guidFileName, fileExtenstion, file.Length);
                    var fileDto = _mapper.Map<FileUplaodDto>(result);
                    response=new BaseResponse<FileUplaodDto>(fileDto);
                    return response;
                }
                else return null;
            }
            catch (Exception ex)
            {
                response = new BaseResponse<FileUplaodDto>(new List<string> { ex.Message }); ;
                return response;
            }
        }

        private async Task<FileToUpload> SaveFile(string path, string name, string type, long size)
        {
            try
            {
                var fileToUpload = new FileToUpload
                {
                    DateCreated = DateTime.Now,
                    Path = path,
                    FileName = name,
                    FileType = type,
                    FileSize = (int)size
                };
                await DbContext.files.AddAsync(fileToUpload);
                await DbContext.SaveChangesAsync();
                return fileToUpload;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

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


        public async Task<BaseResponse<FileUplaodDto>> GetFile(int Id)
        {
            var file = await DbContext.files.FindAsync(Id);
            if (file == null)
                return new BaseResponse<FileUplaodDto>(new List<string> { "No such file found" });
            else
                return new BaseResponse<FileUplaodDto>(_mapper.Map<FileUplaodDto>(file));
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
                    string folderName;
                    string typeName = GetTypeName(contentType, out folderName);

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
                    if (!CheckFileType(contentType, types))
                    {
                        response = new BaseResponse<FileUplaodDto>(new List<string> { "File type is not permited" });
                        return response;
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var result = await SaveFile(dbPath, fileName, guidFileName, typeName, file.Length);
                    var fileDto = _mapper.Map<FileUplaodDto>(result);
                    response = new BaseResponse<FileUplaodDto>(fileDto);
                    return response;
                }
                else
                {
                    response = new BaseResponse<FileUplaodDto>(new List<string> { "No file has been uploaded" });
                    return response;
                };
            }
            catch (Exception ex)
            {
                response = new BaseResponse<FileUplaodDto>(new List<string> { ex.Message }); ;
                return response;
            }
        }

        private bool CheckFileType(string type, string[] allowedTypes)
        {
            var isValid = false;
            foreach (var allowedType in allowedTypes)
            {
                if (type.Contains(allowedType))
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        private async Task<FileToUpload> SaveFile(string path, string name, string dbName, string type, double size)
        {
            try
            {
                var fileToUpload = new FileToUpload
                {
                    DateCreated = DateTime.Now,
                    Path = path,
                    FileName = name,
                    FileDBName = dbName,
                    FileType = type,
                    FileSize = (double)size / 1024
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
        private string GetTypeName(string contentType, out string folderName)
        {
            string typeName = "";
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

            return typeName;
        }
        public async Task<BaseResponse<bool>> DeleteFile(int Id)
        {
            try
            {
                var file = await DbContext.files.FindAsync(Id);
                if (file == null)
                {
                    return new BaseResponse<bool>(new List<string> { "No such file found" });
                }
                DbContext.files.Remove(file);
                DbContext.SaveChanges();

                string typeName = GetTypeName(file.FileType, out string folderName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                string fullPath = Path.Combine(filePath, file.FileDBName);
                if (File.Exists(fullPath))
                    File.Delete(fullPath);

                return new BaseResponse<bool>(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(new List<string> { ex.Message });
            }
        }

        public async Task<string> GetFilePath(int Id)
        {
            var file = await DbContext.files.FindAsync(Id);
            if (file != null)
            {
                string filePath = Path.Combine("Resources","Files", file.FileType);
                string fullPath = Path.Combine(filePath, file.FileDBName);

                return fullPath;
            }
            return null;
        }
    }
}

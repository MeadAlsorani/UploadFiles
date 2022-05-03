using AutoMapper;
using FilesUpload.Application.DTOs;
using FilesUpload.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Upload.Upload
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FileToUpload, FileUplaodDto>();
        }
    }
}

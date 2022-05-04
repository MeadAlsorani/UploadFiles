using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUpload.Application.DTOs
{
    public class FileUplaodDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

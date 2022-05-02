using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUpload.Application.DTOs
{
    public class FileUplaodDto
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

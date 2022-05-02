using FilesUpload.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUpload.Domain
{
    public class FileToUpload : BaseDomainClass
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int FileSize { get; set; }
    }
}

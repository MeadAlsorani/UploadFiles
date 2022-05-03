using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUpload.Application.Responses
{
    public class BaseResponse<T>
    {
        public bool Successful { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public BaseResponse(T Data)
        {
            Successful = true;
            this.Data = Data;
        }
        public BaseResponse(List<string> errors)
        {
            this.Errors = errors;
            this.Successful = false;
        }
    }
}

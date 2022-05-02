using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUpload.Domain.Common
{
    public abstract class BaseDomainClass
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

    }
}

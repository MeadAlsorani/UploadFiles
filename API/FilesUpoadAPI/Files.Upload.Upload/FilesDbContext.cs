using FilesUpload.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Upload.Upload
{
    public class FilesDbContext : DbContext
    {
        public FilesDbContext(DbContextOptions<FilesDbContext> options) : base(options)
        {
        }

        public DbSet<FileToUpload> files { get; set; }
    }
}

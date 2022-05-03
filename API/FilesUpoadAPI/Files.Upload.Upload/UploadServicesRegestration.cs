using Files.Upload.Upload.Repositories;
using FilesUpload.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Upload.Upload
{
    public static class UploadServicesRegestration
    {
        public static IServiceCollection ConfigureUploadServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<FilesDbContext>(options =>
            options.UseSqlServer(
                config.GetConnectionString("SQL"),
                x => x.MigrationsAssembly("FilesUpload.Api"))
            );
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddScoped<IFileUpload, UploadFileRepository>();
            return services;
        }
    }
}

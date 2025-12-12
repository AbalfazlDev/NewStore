using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NewStore.Application.Services.Products.Commands.AddNewProduct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NewStore.Application.Tools
{
    public static class CommonFunctions
    {
        public static UploadDto UploadFile(IHostingEnvironment environment, IFormFile file,string folderName)
        {
            if (file != null)
            {
                if (file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string folder = @$"Images\{folderName}";
                string rootFolder = Path.Combine(environment.WebRootPath, folder);
                if (!Directory.Exists(rootFolder))
                {
                    Directory.CreateDirectory(rootFolder);
                }
                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                string path = Path.Combine(rootFolder, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    Status = true,

                    FileNameAddress = Path.Combine(folder, fileName),
                };
            }
            return null;
        }
    }
}

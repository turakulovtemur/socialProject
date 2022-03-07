using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace socialProject.Services
{
    public class FileService: IFileService
    {
        /// <summary>
        /// Считать массив из файла
        /// </summary>
        /// <param name="file">файл</param>
        /// <returns></returns>
        public async Task<byte[]> ReadDataFromFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }

            return null;
        }

        /// <summary>
        /// Считать данный из файла в виде строки
        /// </summary>
        /// <param name="file">файл</param>
        /// <returns></returns>
        public async Task<string> ReadStringFromFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    return memoryStream.ToString();
                }
            }

            return null;
        }
    }
}

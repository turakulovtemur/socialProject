using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialProject.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Считать массив из файла
        /// </summary>
        /// <param name="file">файл</param>
        /// <returns></returns>
        Task<byte[]> ReadDataFromFile(IFormFile file);

        /// <summary>
        /// Считать данный из файла в виде строки
        /// </summary>
        /// <param name="file">файл</param>
        /// <returns></returns>
        Task<string> ReadStringFromFile(IFormFile file);
    }
}

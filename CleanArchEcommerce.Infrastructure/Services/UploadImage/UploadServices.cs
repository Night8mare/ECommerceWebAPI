using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Services.UploadImage
{
    public class UploadServices : IUploadServices
    {
        #region Field
        private readonly string _storage;
        private readonly string _defualt;
        #endregion
        #region Constructor
        public UploadServices(IConfiguration configuration)
        {
            _storage = configuration.GetValue<string>("ImageStoragePath");
            _defualt = configuration.GetValue<string>("DefaultImagePath");
        }
        #endregion
        #region Handler Function
        public async Task<string> LocalUploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return _defualt;
            var filePath = Path.Combine(_storage, file.FileName);
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log them, rethrow, etc.)
                throw new Exception("An error occurred while uploading the file.", ex);
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            Log.Information($"file bytes: {fileBytes}");
            return filePath;
        }
        #endregion
    }
}

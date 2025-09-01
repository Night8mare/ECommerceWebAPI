using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Services.UploadImage
{
    public interface IUploadServices
    {
        Task<string> LocalUploadImage(IFormFile file);
    }
}

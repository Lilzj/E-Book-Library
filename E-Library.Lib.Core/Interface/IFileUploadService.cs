﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Interface
{
    public interface IFileUploadService
    {
        string UploadImage(IFormFile file);
    }
}

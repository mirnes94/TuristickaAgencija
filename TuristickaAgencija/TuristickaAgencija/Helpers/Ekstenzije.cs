using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TuristickaAgencija.Helpers
{
    public static class Ekstenzije
    {
        public static byte[] ToByteArray(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}


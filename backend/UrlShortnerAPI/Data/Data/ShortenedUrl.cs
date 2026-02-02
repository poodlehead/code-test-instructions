using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class ShortenedUrl
    {
        public required string shortString { get; set; }
        public required string Url { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileDownloader.Models
{
    public class ImageDownloaderRequest
    {
        public int Id { get; set; }
    }

    public class ImagesRequest : List<ImageDownloaderRequest>
    {
        
    }
}
using FileDownloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FileDownloader.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage DownloadSingleFile(string id, string name)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Content\\images\\{id}.jpg";
                if (File.Exists(fullPath))
                {

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileStream = new FileStream(fullPath, FileMode.Open);
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = name;                    
                    return response;
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpGet]
        public HttpResponseMessage DownloadMultipleFile(ImagesRequest data)
        {
            var content = new MultipartContent();
            var ids = new List<int>();
            List<Image> images = new List<Image>();
            foreach (var obj in data)
            {
                ids.Add(obj.Id);
                images.Add(ImageList.GetImage(obj.Id));
            }
            var objectContent = new ObjectContent<List<int>>(ids, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            content.Add(objectContent);
            foreach (Image img in images)
            {
                ids.Add(img.Id);

                string fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}/images/{img.Id}.jpg";

                var file1Content = new StreamContent(new FileStream(fullPath, FileMode.Open));
                file1Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");
                content.Add(file1Content);

                //var file2Content = new StreamContent(new FileStream(@"c:\temp\test.txt", FileMode.Open));
                //file2Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain");
                //content.Add(file2Content);                
            }
            var response = new HttpResponseMessage();
            response.Content = content;
            return response ?? new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

using FileDownloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.IO.Compression;
using Ionic.Zip;

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

        [HttpPost]
        public HttpResponseMessage DownloadMultipleFile(ImagesRequest data)
        {
            var ids = new List<int>();
            List<Image> images = new List<Image>();
            foreach (var obj in data)
            {
                ids.Add(obj.Id);
                images.Add(ImageList.GetImage(obj.Id));
            }

            var archive = AppDomain.CurrentDomain.BaseDirectory + "/archive.zip";
            var temp = AppDomain.CurrentDomain.BaseDirectory + "/temp";

            // clear any existing archive
            if (System.IO.File.Exists(archive))
            {
                System.IO.File.Delete(archive);
            }

            // empty the temp folder
            //Directory.EnumerateFiles(temp).ToList().ForEach(f => System.IO.File.Delete(f));
            
            using (ZipFile zip = new ZipFile())
            {
                //zip.word = word;
                foreach (var img in images)
                {
                    string fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Content\\images\\{img.Id}.jpg";
                    //System.IO.File.Copy(fullPath, Path.Combine(temp, Path.GetFileName(fullPath)));
                    zip.AddFile(fullPath);
                }

                zip.Save(temp + "\\" +"ZipDownload.zip");
                var pushStreamContent = new PushStreamContent((stream, content, context) =>
                {
                    zip.Save(stream);
                    stream.Close(); // After save we close the stream to signal that we are done writing.
                }, "application/zip");

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = pushStreamContent };
            }
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

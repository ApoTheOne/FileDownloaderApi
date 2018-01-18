using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileDownloader.Models
{
    public class ImageList : List<Image>
    {
        private static ImageList images;

        public static ImageList Images { get { return images; } }

        public static void InitializeImages()
        {
            if (images == null || images.Count == 0)
            {
                images = new ImageList();
                images.Add(new Image() { Id = 1, Name = "Dolphin", Path = @"\Content\images\1.JPG", Selected = false });
                images.Add(new Image() { Id = 2, Name = "Telemetry", Path = @"\Content\images\2.JPG", Selected = false });
                images.Add(new Image() { Id = 3, Name = "Na", Path = @"\Content\images\3.JPG", Selected = false });
            }
        }

        public static Image GetImage(int id)
        {
            return images.FirstOrDefault(x => x.Id == id);
        }
    }

    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace Diploma.Repository
{
    public class ImageRepository
    {
        public byte[]? GetImage(string dir, string id)
        {
            var path = $"{dir}{id}.jpg";
            if (File.Exists(path))
                return File.ReadAllBytes(path);
            else
                return null;
        }

        public void PostImage(byte[] imgBytes, string dir, string id)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var image = Image.Load(imgBytes);

            image.Mutate(m =>
                m.Resize(
                    new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(512)
                    }
                 )
            );
            image.Save($"{dir}{id}.jpg");
        }
    }
}

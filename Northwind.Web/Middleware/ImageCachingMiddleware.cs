using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Web.Middleware
{
    public class ImageCachingMiddleware
    {
        private RequestDelegate nextDelegate;
        private string cacheDirectory;
        private int maxImages;

        public ImageCachingMiddleware(RequestDelegate next, string cacheDirectory, int maxImages)
        {
            this.nextDelegate = next;
            this.cacheDirectory = cacheDirectory;
            this.maxImages = maxImages;

            PhysicalFileProvider fileProvider = new PhysicalFileProvider(cacheDirectory);
        }

        public async Task Invoke(HttpContext context)
        {
            string url = context.Request.Path;
            string imageGuid = url.GetHashCode().ToString();
            var files = Directory.EnumerateFiles(cacheDirectory);
            string fileName = files.FirstOrDefault(f => f.StartsWith(Path.Combine(cacheDirectory, imageGuid)));
            if (!string.IsNullOrEmpty(fileName))
            {
                using (var stream = new FileStream(fileName, FileMode.Open))
                {
                    await stream.CopyToAsync(context.Response.Body);
                    return;
                }
            }

            Stream originalBody = context.Response.Body;
            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;

                await nextDelegate.Invoke(context);

                string contentType = context.Response.ContentType;
                if (contentType.StartsWith("image")
                    && !string.IsNullOrWhiteSpace(imageGuid))
                {
                    string imageExtension = contentType.Substring(contentType.IndexOf('/') + 1);
                    string imageName = string.Concat(imageGuid, ".", imageExtension);
                    string path = Path.Combine(cacheDirectory, imageName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        memStream.Position = 0;
                        await memStream.CopyToAsync(stream);
                    }
                }

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
            }
            

            
        }
    }
}

﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Web.Middleware
{
	public class ImageCachingMiddleware
	{
		private RequestDelegate nextDelegate;
		private readonly string cacheDirectory;
		private readonly int maxImages;
		private object syncObj;
		private DateTime lastRequestTime;

		public ImageCachingMiddleware(RequestDelegate next, string cacheDirectory, int maxImages)
		{
			this.nextDelegate = next;
			this.cacheDirectory = cacheDirectory;
			this.maxImages = maxImages;

			PhysicalFileProvider fileProvider = new PhysicalFileProvider(cacheDirectory);
			//ThreadPool.QueueUserWorkItem(CleanCache);
		}

		public async Task Invoke(HttpContext context)
		{
			this.lastRequestTime = DateTime.Now;
			Stream originalBody = context.Response.Body;
			using (var memStream = new MemoryStream())
			{
				string url = context.Request.Path;
				string imageGuid = url.Split("/").Last();

				await this.GetFromCache(context, imageGuid);

				context.Response.Body = memStream;

				await nextDelegate.Invoke(context);

				await SaveToCache(context, imageGuid);

				memStream.Position = 0;
				await memStream.CopyToAsync(originalBody);
			}
		}

		private async Task GetFromCache(HttpContext context, string imageGuid)
		{
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
		}

		private async Task SaveToCache(HttpContext context, string imageGuid)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(cacheDirectory);
			if (directoryInfo.EnumerateFiles().Count() >= this.maxImages)
			{
				return;
			}

			string contentType = context.Response.ContentType;
			if (!string.IsNullOrEmpty(contentType)
				&& contentType.StartsWith("image")
				&& !string.IsNullOrWhiteSpace(imageGuid))
			{
				string imageExtension = contentType.Substring(contentType.IndexOf('/') + 1);
				string imageName = string.Concat(imageGuid, ".", imageExtension);
				string path = Path.Combine(cacheDirectory, imageName);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					context.Response.Body.Position = 0;
					await context.Response.Body.CopyToAsync(stream);
				}
			}
		}

		public void CleanCache(object state)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(cacheDirectory);
			while (true)
			{
				DateTime cacheLive = DateTime.Now.AddSeconds(-20);
				if (lastRequestTime.CompareTo(cacheLive) < 0)
				{
					foreach (var file in directoryInfo.EnumerateFiles())
					{
						file.Delete();
					}
				}
				Thread.Sleep(20000);
				
			}
		}
    }
}

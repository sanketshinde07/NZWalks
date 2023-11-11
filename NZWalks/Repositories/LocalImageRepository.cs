using System;
using NZWalks.Models.Domain;
using NZWalks.Data;

namespace NZWalks.Repositories
{
	public class LocalImageRepository:IImageRepository
	{
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext nZwalksDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,NZWalksDbContext nZwalksDbContext,IHttpContextAccessor httpContextAccessor)
		{
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZwalksDbContext = nZwalksDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            //upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}" +
              $"{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await nZwalksDbContext.Images.AddAsync(image);
            await nZwalksDbContext.SaveChangesAsync();

            return image;

        }
    }
}


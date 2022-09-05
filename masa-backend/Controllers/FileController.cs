using masa_backend.ModelViews.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace masa_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileProvider fileProvider;

        public FileController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public IActionResult Files()
        {
            var model = new FilesViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            return Ok(model);
        }
        [HttpGet,Route("[action]/{filename}/{userId}")]
        public IActionResult Download([FromRoute]string filename,[FromRoute]string userId)
        {
            var memory = new MemoryStream();
            try
            {
                if (filename == null)
                    return Content("filename not present");

                var path = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot", "Files", userId, filename);
                //using (var stream = new FileStream(path, FileMode.Open))
                //{
                //    await stream.CopyToAsync(memory);
                //}
                //memory.Position = 0;
                //var photos = Directory.EnumerateFiles(path)
                //    .Where(current=>current.Contains(filename))
                //    .Select(fullPath => Path.Combine(path, filename));
                return Ok(new { path });
                //return Ok(File(memory, GetContentType(path), Path.GetFileName(path)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                memory.Dispose();
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}

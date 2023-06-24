using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace DirectoryServer.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase {
        IConfiguration _configuration;
        string basePath;
        public FilesController(IConfiguration configuration) {
            _configuration = configuration;
            basePath = _configuration.GetSection("BasePath").Value;
        }

        [HttpGet("getfile")]
        public async Task<IActionResult> Download(string fileName) {

        
            fileName = Path.GetFileName(fileName);

            var fullPath = Path.Combine(basePath, fileName);


            var stream = new FileStream(fullPath, FileMode.Open);

            if(stream == null)
                return NotFound();

            return File(stream, "application/octet-stream", Path.GetFileName(fullPath)); // returns a FileStreamResult
        }

        private bool IsFileNameValid(string file) {

            if(string.IsNullOrEmpty(file))
                return false;


            if(file.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                return false;

            if(file.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                return false;

            return true;


        }
    }
}
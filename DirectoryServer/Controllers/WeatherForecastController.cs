using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace DirectoryServer.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {
        

        [HttpGet("getfile")]
        public async Task<IActionResult> Download(string fileName) {

            FileStream stream = new FileStream(fileName,FileMode.Open);

            if(stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.

            return File(stream, "application/octet-stream", Path.GetFileName(fileName)); // returns a FileStreamResult
        }
    }
}
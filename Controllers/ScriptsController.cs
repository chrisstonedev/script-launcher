using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ScriptLauncher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScriptsController : ControllerBase
    {
        private readonly ILogger<ScriptsController> _logger;

        public ScriptsController(ILogger<ScriptsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Script> Get()
        {
            var files = Directory
                .EnumerateFiles("C:\\code")
                .Where(f => f.EndsWith(".sh"))
                .Select(x => new Script
                {
                    FilePath = x,
                    FileName = Path.GetFileName(x)
                });
            return files.ToArray();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Script script)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = @"C:\Program Files\Git\usr\bin\bash.exe",
                    WorkingDirectory = @"C:\code",
                    Arguments = script.FilePath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };
            process.Start();

            var error = process.StandardError.ReadToEnd();
            var output = process.StandardOutput.ReadToEnd();

            var fileExists = System.IO.File.Exists(script.FilePath);
            _logger.Log(LogLevel.Information, "File exists: " + fileExists);
            return Ok(new
            {
                fileExists,
                output,
                error
            });
        }
    }
}
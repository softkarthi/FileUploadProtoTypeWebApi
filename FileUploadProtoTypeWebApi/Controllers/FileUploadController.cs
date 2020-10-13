using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileUploadProtoTypeWebApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileUploadProtoTypeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger<FileUploadController> _logger;
        private readonly IFileService _fileService;

        public FileUploadController(ILogger<FileUploadController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(IFormFile formFile)
        {
            bool result = await _fileService.WriteFile(formFile);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to upload file");
            }
            return Ok("success");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            return Ok(_fileService.GetFiles());
        }
    }
}
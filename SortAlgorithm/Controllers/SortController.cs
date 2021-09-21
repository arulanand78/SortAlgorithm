using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service;
using SortAlgorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SortAlgorithm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SortController : ControllerBase
    {
        private readonly ILogger<SortController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IFileOperation _fileIO;
        private IHostEnvironment _hostEnvironment;
        private readonly AppSettings _appSettings;
        private readonly IAlgorithmFactory _algorithmFactory;

        public SortController(ILogger<SortController> logger, IServiceProvider serviceProvider, IFileOperation fileIO, IHostEnvironment hostEnvironment, 
            IOptions<AppSettings> appSettings, IAlgorithmFactory algorithmFactory)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _fileIO = fileIO;
            _hostEnvironment = hostEnvironment;
            _appSettings = appSettings.Value;
            _algorithmFactory = algorithmFactory;
        }

        [HttpGet]
        public ActionResult Get()
        {
            string result;
            var isFileRead = _fileIO.Read(_appSettings.ResultFileName, _hostEnvironment.ContentRootPath + @"\AppData", out result);
            if (isFileRead)
            {
                return Content(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        public  ActionResult SortInput([FromBody] int[] unsortedIntegers, string algorithmType)
        {
            if (unsortedIntegers.Length == 0 || algorithmType.Length <= 0)
                return BadRequest("Invalid Input, Please check the input passed");

            var _sortAlgorithm = _algorithmFactory.GetSortAlgorithm(algorithmType);
            if (_sortAlgorithm != null)
            {
                var sortedIntegers =  _sortAlgorithm.DoSort(unsortedIntegers.ToArray());
                SortResultSummary result = new SortResultSummary
                {
                    FileName = _appSettings.ResultFileName,
                    FilePath = _hostEnvironment.ContentRootPath + @"\AppData",
                    OutputData = sortedIntegers,
                    InputData = unsortedIntegers,
                    SortAlgorithm = (SortType.Algorithm)Enum.Parse(typeof(SortType.Algorithm), algorithmType)
                };

                bool isFileCreated = _fileIO.Write(result);
                if (isFileCreated)
                {
                    return CreatedAtAction(nameof(SortInput), sortedIntegers);
                }
                else
                {
                    return BadRequest("An error occured, File was not created successfully");
                }
            }
            else
            {
                return BadRequest("Entered algorithm type is not supported, Please enter `Bubblesort` or `Mergesort`");
            }
        }
    }
}

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
        private ISortAlgorithm _sortAlgorithm;
        private FileOperation _fileIO;
        private IHostEnvironment _hostEnvironment;
        private readonly AppSettings _appSettings;

        public SortController(ILogger<SortController> logger, IServiceProvider serviceProvider, FileOperation fileIO, IHostEnvironment hostEnvironment, 
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _fileIO = fileIO;
            _hostEnvironment = hostEnvironment;
            _appSettings = appSettings.Value;
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
        public async Task<ActionResult> SortInput([FromBody] int[] unsortedIntegers, string sortAlgorithm)
        {
            if (unsortedIntegers.Length == 0 || sortAlgorithm.Length <= 0)
                return BadRequest("Invalid Input, Please check the input passed");

            
            if (sortAlgorithm == SortType.Algorithm.Bubblesort.ToString())
            {
                _sortAlgorithm = _serviceProvider.GetService<BubbleSortAlgorithm>();
            }
            else
            {
                return BadRequest("Invalid Sort Algorithm, Enter either Bubblesort or ");
            }


            int[] sortedIntegers = await _sortAlgorithm.DoSort(unsortedIntegers.ToArray());
            SortResultSummary result = new SortResultSummary 
                                    { 
                                        FileName = _appSettings.ResultFileName,
                                        FilePath = _hostEnvironment.ContentRootPath + @"\AppData",
                                        OutputData = sortedIntegers,
                                        InputData = unsortedIntegers,
                                        SortAlgorithm = (SortType.Algorithm) Enum.Parse(typeof(SortType.Algorithm), sortAlgorithm)
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
    }
}

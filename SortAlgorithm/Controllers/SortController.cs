using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service;
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

        public SortController(ILogger<SortController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Content("");
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


            int[] sortedIntegers = await _sortAlgorithm.DoSort(unsortedIntegers);
            return CreatedAtAction(nameof(SortInput), sortedIntegers);
        }
    }
}

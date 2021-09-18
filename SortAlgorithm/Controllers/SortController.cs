using Microsoft.AspNetCore.Mvc;
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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SortController> _logger;
        private readonly ISortAlgorithm _sortAlgorithm;

        public SortController(ILogger<SortController> logger)
        {
            _logger = logger;
            _sortAlgorithm = new BubbleSortAlgorithm();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Content("");
        }

        [HttpPost]
        public async Task<ActionResult> SortInput([FromBody] int[] unsortedIntegers)
        {
            if (unsortedIntegers.Length == 0)
                return BadRequest();

            int[] sortedIntegers = await _sortAlgorithm.DoSort(unsortedIntegers);
            return CreatedAtAction(nameof(SortInput), sortedIntegers);
        }
    }
}

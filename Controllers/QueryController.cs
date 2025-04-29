using Microsoft.AspNetCore.Mvc;
using SqlApiForCopilotTest.Models;
using SqlApiForCopilotTest.Providers;

namespace SqlApiForCopilotTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IValidationProvider _validationProvider;
        public QueryController(IValidationProvider validationProvider)
        {
            _validationProvider = validationProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QueryRequest request)
        {
            try
            {
                var result = await _validationProvider.QueryTableAsync(request.TableName, request.Columns);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

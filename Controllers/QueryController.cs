using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using SqlApiForCopilotTest.Models;

namespace SqlApiForCopilotTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IConfiguration _config;
        public QueryController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QueryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TableName) || request.Columns == null || !request.Columns.Any())
                return BadRequest("Invalid input.");

            // Prevent SQL injection by allowing only alphanumeric and underscore in table/column names
            if (!System.Text.RegularExpressions.Regex.IsMatch(request.TableName, @"^\w+$") ||
                request.Columns.Any(c => !System.Text.RegularExpressions.Regex.IsMatch(c, @"^\w+$")))
                return BadRequest("Invalid table or column name.");

            var columns = string.Join(", ", request.Columns.Select(c => $"[{c}]").ToArray());
            var sql = $"SELECT {columns} FROM [{request.TableName}]";

            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var result = (await conn.QueryAsync(sql)).Select(row => (IDictionary<string, object>)row).ToList();
            return Ok(result);
        }
    }
}

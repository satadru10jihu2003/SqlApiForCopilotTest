using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SqlApiForCopilotTest.Repository
{
    public class QueryRepository : IQueryRepository
    {
        private readonly IConfiguration _config;
        public QueryRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<IDictionary<string, object>>> QueryTableAsync(string tableName, List<string> columns)
        {
            if (string.IsNullOrWhiteSpace(tableName) || columns == null || !columns.Any())
                throw new ArgumentException("Invalid input.");

            // Prevent SQL injection by allowing only alphanumeric and underscore in table/column names
            if (!System.Text.RegularExpressions.Regex.IsMatch(tableName, @"^\w+$") ||
                columns.Any(c => !System.Text.RegularExpressions.Regex.IsMatch(c, @"^\w+$")))
                throw new ArgumentException("Invalid table or column name.");

            var cols = string.Join(", ", columns.Select(c => $"[{c}]").ToArray());
            var sql = $"SELECT {cols} FROM [{tableName}]";

            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var result = (await conn.QueryAsync(sql)).Select(row => (IDictionary<string, object>)row).ToList();
            return result;
        }
    }
}

using System.Text.RegularExpressions;
using SqlApiForCopilotTest.Repository;

namespace SqlApiForCopilotTest.Providers
{
    public class ValidationProvider : IValidationProvider
    {
        private static readonly Regex NameRegex = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$", RegexOptions.Compiled);
        private readonly IQueryRepository _queryRepository;
        public ValidationProvider(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public void ValidateTableName(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName) || !NameRegex.IsMatch(tableName))
                throw new ArgumentException("Invalid table name.");
        }

        public void ValidateColumnNames(IEnumerable<string> columnNames)
        {
            if (columnNames == null || !columnNames.Any())
                throw new ArgumentException("Column list cannot be empty.");
            foreach (var col in columnNames)
            {
                if (string.IsNullOrWhiteSpace(col) || !NameRegex.IsMatch(col))
                    throw new ArgumentException($"Invalid column name: {col}");
            }
        }

        public async Task<List<IDictionary<string, object>>> QueryTableAsync(string tableName, List<string> columns)
        {
            ValidateTableName(tableName);
            ValidateColumnNames(columns);
            return await _queryRepository.QueryTableAsync(tableName, columns);
        }
    }
}
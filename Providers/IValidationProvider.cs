namespace SqlApiForCopilotTest.Providers
{
    public interface IValidationProvider
    {
        void ValidateTableName(string tableName);
        void ValidateColumnNames(IEnumerable<string> columnNames);
        Task<List<IDictionary<string, object>>> QueryTableAsync(string tableName, List<string> columns);
    }
}
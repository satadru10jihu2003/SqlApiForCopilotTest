namespace SqlApiForCopilotTest.Repository
{
    public interface IQueryRepository
    {
        Task<List<IDictionary<string, object>>> QueryTableAsync(string tableName, List<string> columns);
    }
}

namespace SqlApiForCopilotTest.Models
{
    public class QueryRequest
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
    }
}
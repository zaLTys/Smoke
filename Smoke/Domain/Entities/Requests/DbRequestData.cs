using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public class DbRequestData
    {
        public DbType DbType { get; set; }
        public string ConnectionString { get; set; }
        public string Query { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public object ExpectedResult { get; set; }

        public DbRequestData(DbType dbType, string connectionString, string query, Dictionary<string, object> parameters, object expectedResult)
        {
            DbType = dbType;
            ConnectionString = connectionString;
            Query = query;
            Parameters = parameters;
            ExpectedResult = expectedResult;
        }
    }

}

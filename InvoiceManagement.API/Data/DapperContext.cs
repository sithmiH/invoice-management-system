using Microsoft.Data.SqlClient;
using System.Data;

namespace InvoiceManagement.API.Data;

// Provides database connections to repositories using the connection string
public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Creates a new SQL Server database connection
    public IDbConnection CreateConnection()
    {
        return new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));
    }
}

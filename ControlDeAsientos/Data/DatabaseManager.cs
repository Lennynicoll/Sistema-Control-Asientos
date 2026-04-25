using Microsoft.Data.SqlClient;
using System.Data;

namespace ControlDeAsientos.Data;

public class DatabaseManager : IDisposable
{
    private readonly string _connectionString;
    private SqlConnection? _connection;

    public DatabaseManager()
    {
        _connectionString = @"Server=localhost\SQLEXPRESS01;Database=SistemaDeControlAsientosDB;Trusted_Connection=True;TrustServerCertificate=True;";
    }

    public SqlConnection GetConnection()
    {
        if (_connection == null || _connection.State == ConnectionState.Closed)
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }
        return _connection;
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            _connection.Dispose();
            _connection = null;
        }
    }
}

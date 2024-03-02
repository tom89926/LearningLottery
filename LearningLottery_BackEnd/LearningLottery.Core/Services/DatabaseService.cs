using System.Data.SqlClient;
using LearningLottery.Core.Services.Models.MetaData;


namespace LearningLottery.Core.Services;


public class DatabaseService : IDatabaseService {
    private readonly SqlConnection _sqlConnection;
    
    public DatabaseService() {
        _sqlConnection = new SqlConnection(Config.SqlDbConnection);
    }

    public SqlConnection GetConnection() {
        return _sqlConnection;
    }
}

public interface IDatabaseService {
    SqlConnection GetConnection();
}
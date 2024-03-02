using Dapper;
using LearningLottery.Core.Repositories.Models;
using LearningLottery.Core.Services.Models.MetaData;
using Microsoft.Data.SqlClient;

namespace LearningLottery.Core.Repositories;

public class BaseRepo
{
    protected SqlDbContext _sqlDbContext;

    public BaseRepo(SqlDbContext sqlDbContext)
    {
        _sqlDbContext = sqlDbContext;
    }

    protected Boolean CheckDbSaveChange()
    {
        var saveChanges = _sqlDbContext.SaveChanges();
        if (saveChanges == -1)
        {
            return false;
        }

        return true;
    }
    protected static string ConnectString => Config.SqlDbConnection ?? "";

    protected T DoConnection<T>(Func<SqlConnection, T> func) {
        var test = ConnectString;
        var conn = new SqlConnection(ConnectString);
        conn.Open();

        var result = func.Invoke(conn); // 回傳1

        conn.Close();

        return result;
    }
    
    public IEnumerable<T> Query<T>(string sql, object param = null)
    {
        using (var cn = new SqlConnection(ConnectString))
        {
            return cn.Query<T>(sql, param);
        }
    }
    public void Execute(string sql, object param)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            conn.Execute(sql, param);
        }
    }
}
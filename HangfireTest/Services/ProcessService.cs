using Hangfire;
using System.Configuration;
using System.Data.SqlClient;

namespace HangfireTest.Services
{
    public class ProcessService
    {
        [AutomaticRetry(Attempts = 0)] // Prevents Hangfire from auto-retrying failed jobs
        public int SlowRunningProcess()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["development"].ConnectionString))
            using (var cmd = connection.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "exec [sp_SlowRunningProcess]";
                    cmd.CommandTimeout = 60 * 3;
                    connection.Open();
                    var result = (int)cmd.ExecuteScalar();
                    return result;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
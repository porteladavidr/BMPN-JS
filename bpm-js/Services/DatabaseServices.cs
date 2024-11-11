using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bpm_js.Services
{
    public class DatabaseServices
    {
        private string connectionString = "user id=callback;Server=10.1.2.225;persist security info=False;initial catalog=BPMN_DEMO; password=softium";

        public void Insert(string name)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = $"INSERT INTO DemoTable VALUES('{name}')";

            command.ExecuteNonQuery();


            sqlConnection.Close();
            command.Dispose();
            sqlConnection.Dispose();


        }

    }
}
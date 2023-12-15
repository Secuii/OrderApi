using MySql.Data.MySqlClient;
using System.Data;

namespace OrderAPI.Command
{
    public interface ICommand<Request, Response> where Request : DataBase.Request where Response : DataBase.Response
    {
        public string GetCommand();
        public MySqlParameter[] Parameters(Request request);

        public Response ExtractData(DataTable table, Request request);
    }
}

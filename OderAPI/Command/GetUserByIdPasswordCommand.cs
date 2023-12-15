using OrderAPI.Responses;
using OrderAPI.Requests;
using System.Data;
using MySql.Data.MySqlClient;

namespace OrderAPI.Command
{
    public class GetUserByIdPasswordCommand : ICommand<LoginAppRequest, LoginAppResponse>
    {
        public string GetCommand() => "Not implemented";

        public MySqlParameter[] Parameters(LoginAppRequest request) => new MySqlParameter[]
        {
            new MySqlParameter("",""),
            new MySqlParameter("","")
        };
         
        public LoginAppResponse ExtractData(DataTable table, LoginAppRequest request)
        {
            LoginAppResponse response = new LoginAppResponse();
            response.id = 1;
            response.Name = " Amdres";

            return response;
        }

    }
}

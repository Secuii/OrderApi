using MySql.Data.MySqlClient;
using OrderAPI.Command;
using System.Data;

namespace OrderAPI.DataBase
{
    public class CommandResolver : ICommandResolver
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

#pragma warning disable CS8603
        private string SqlDataSource() => _configuration.GetConnectionString("OrderDataBase");
#pragma warning restore CS8603


        public CommandResolver(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public Response Resolve<Request, Response>(Request request) where Request : DataBase.Request where Response : DataBase.Response
        {
            var command = GetICommandService<Request, Response>();
            var data = command.ExtractData(GetTableFromDataBase(command, request), request);
            return data;
        }
#pragma warning disable CS8603 // Possible null reference return.
        public ICommand<Request, Response> GetICommandService<Request, Response>() where Request : DataBase.Request where Response : DataBase.Response => _serviceProvider.GetService<ICommand<Request, Response>>();
#pragma warning restore CS8603 // Possible null reference return.

        public DataTable GetTableFromDataBase<Request, Response>(ICommand<Request, Response> command, Request request) where Request : DataBase.Request where Response : DataBase.Response
        {
            DataTable table = new DataTable();

            MySqlDataReader sqlReader;
            using (MySqlConnection sqlConnection = new(SqlDataSource()))
            {
                sqlConnection.Open();

                using MySqlCommand sqlCommand = new(command.GetCommand(), sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                ImplementParamters(sqlCommand, command.Parameters(request));

                sqlReader = sqlCommand.ExecuteReader();
                table.Load(sqlReader);

                sqlReader.Close();
                sqlConnection.Close();
            }
            return table;
        }

        public void ImplementParamters(MySqlCommand sqlCommand, MySqlParameter[] parameters)
        {
            foreach (var parameter in parameters)
            {
                sqlCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
            }
        }
    }
}

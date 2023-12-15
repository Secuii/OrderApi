using MySql.Data.MySqlClient;
using OrderAPI.Requests;
using OrderAPI.Responses;
using System.Data;

namespace OrderAPI.Command
{
    public class GetAllItemsCommand : ICommand<GetAllItemsRequest, GetAllItemsResponse>
    {
        public string GetCommand() => "Items_GetAll";

        public MySqlParameter[] Parameters(GetAllItemsRequest request) => new MySqlParameter[]
        {
          new MySqlParameter("",""),
          new MySqlParameter("",""),
        };
        public GetAllItemsResponse ExtractData(DataTable table, GetAllItemsRequest request)
        {
            var result = new GetAllItemsResponse();
            foreach (DataRow row in table.Rows)
            {
                var id = (int)row["id"];
                string eng_name = (string)row["eng_name"];
                string esp_name = (string)row["esp_name"];
                result.Items.Add(new Item(id, eng_name, esp_name));
            }
            return result;
        }

    }
}

using OrderAPI.DataBase;
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace OrderAPI.Responses
{
    [Serializable]
    public class LoginAppResponse : Response
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}

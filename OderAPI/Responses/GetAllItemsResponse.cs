using OrderAPI.DataBase;

namespace OrderAPI.Responses
{
    [Serializable]
    public class GetAllItemsResponse : Response
    {
        public List<Item> Items { get; set; } = new List<Item>();
    }
}

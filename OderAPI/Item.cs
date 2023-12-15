namespace OrderAPI
{
    [Serializable]
    public class Item
    {
        public Item(int id, string eng_name, string esp_name)
        {
            this.id = id;
            this.eng_name = eng_name;
            this.esp_name = esp_name;
        }

        public int id { get; set; }
        public string eng_name { get; set; } 
        public string esp_name { get; set; }
    }
}

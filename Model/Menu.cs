namespace DataServerRestaurant.Model
{
    public class Menu
    {
        public long MenuId { get; set; }
        public int Price { get; set; }

        public Menu()
        {
        }
        
        public Menu(int price)
        {
            Price = price;
        }
    }
}
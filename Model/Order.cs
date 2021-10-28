namespace DataServerRestaurant.Model
{
    public class Order
    {
         public long OrderId { get; set; }
         public string Description { get; set; }
         public string Status { get; set; } // How do I work with enums?
         public Customer Customer { get; set; }
         public Menu Menu { get; set; }
         public int Price { get; set; }

         public Order()
         {
             
         }

         public Order(string description, string status, Customer customer, Menu menu, int price)
         {
             Description = description;
             Status = status;
             Customer = customer;
             Menu = menu;
             Price = price;
         }
         
    }
    

}
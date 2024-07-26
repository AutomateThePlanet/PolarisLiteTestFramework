namespace DemoSystemTests.Framework.Web.Pages.Models;
public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public bool Discontinued { get; set; }
    public DateTime LastSupply { get; set; }
    public int UnitsOnOrder { get; set; }
    public Category Category { get; set; }
    public int CategoryID { get; set; }
    public int CountryID { get; set; }
    public string QuantityPerUnit { get; set; }
    public Country Country { get; set; }
    public int CustomerRating { get; set; }
    public int TargetSales { get; set; }
    public int TotalSales { get; set; }
}

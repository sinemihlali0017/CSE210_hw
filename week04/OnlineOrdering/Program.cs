using System;
using System.Collections.Generic;

public class Adress
{
    public string Country;
    public string City;
    public string Street;

    public Adress(string country, string city, string street)
    {
        Country = country;
        City = city;
        Street = street;
    }

    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {Country}";
    }
}


public class Customer
{
    public string Name;
    public Adress Address;

    public Customer(string name, Adress address)
    {
        Name = name;
        Address = address;
    }

    public bool LivesInUSA()
    {
        return Address.IsInUSA();
    }
}

public class Product
{
    public string Name;
    public double Price;
    public int Quantity;
    public int Id;

    public Product(string name, double price, int quantity, int id)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Id = id;
    }

    public double GetTotalPrice()
    {
        return Price * Quantity;
    }

    public string GetPackingInfo()
    {
        return $"{Name} (ID: {Id})";
    }
}

public class Order
{
    public List<Product> Products;
    public Customer Customer;

    public Order(Customer customer)
    {
        Customer = customer;
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = 0;
        foreach (Product product in Products)
        {
            total += product.GetTotalPrice();
        }


        if (Customer.LivesInUSA())
        {
            total += 5;
        }
        else
        {
            total += 35;
        }

        return total;
    }

    public string GetPackingLabel()
    {
        string label = "";
        foreach (Product product in Products)
        {
            label += product.GetPackingInfo() + "\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"{Customer.Name}\n{Customer.Address.GetFullAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the OnlineOrdering Project.");


        Adress address1 = new Adress("USA", "Utah", "123 Main St");
        Customer customer1 = new Customer("ALI", address1);

        Product product1 = new Product("Laptop", 1000, 1, 101);
        Product product2 = new Product("Mouse", 20, 2, 102);

        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Console.WriteLine("\nPacking Label:");
        Console.WriteLine(order1.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());

        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():0.00}");
    }
}

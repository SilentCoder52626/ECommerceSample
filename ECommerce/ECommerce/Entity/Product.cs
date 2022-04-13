using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity
{
    public class Product
    {
        protected Product()
        {

        }
        public const string AvailabilityStatusAvailable = "AVAILABLE";
        public const string AvailabilityStatusUnAvailable = "UNAVAILABLE";
        public Product(Category category,Brand brand,Tag tag, string name,decimal price,string color, string sku,string image,string desc)
        {
            Brand = brand;
            Tag = tag;
            Name = name;
            UpdatePrice(price);
            Color = color;
            Category = category;
            SKU = sku;
            Image = image;
            Description = desc;
            CreatedDate = DateTime.Now;
            SetAsAvailable();
        }
        public void Update(Category category, Brand brand, Tag tag, string name, decimal price, string color, string sku, string image, string desc)
        {
            Brand = brand;
            Tag = tag;
            Name = name;
            UpdatePrice(price);
            Color = color;
            Category = category;
            SKU = sku;
            Image = image;
            Description = desc;
        }
        public long ProductId { get; protected set; }
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public void UpdatePrice(decimal price)
        {
            if (price < 0) throw new Exception("Price Must be greater than zero.");
            Price = price;
        }
        public string Description { get; protected set; }
        public string SKU { get; protected set; }
        public string Image { get; protected set; }
        public string Color { get; protected set; }
        public string AvailabilityStatus { get; protected set; }
        public void SetAsAvailable()
        {
            this.AvailabilityStatus = AvailabilityStatusAvailable;
        }
        public void SetAsUnAvailable()
        {
            this.AvailabilityStatus = AvailabilityStatusUnAvailable;
        }
        public DateTime CreatedDate { get; protected set; } 
        public Brand Brand { get; protected set; }
        public long BrandId { get; protected set; }
        public Category Category { get; protected set; }
        public long CategoryId { get; protected set; }
        public Tag Tag { get; protected set; }
        public long TagId { get; protected set; }


    }
}

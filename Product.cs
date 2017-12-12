using System;
using Newtonsoft.Json;


namespace refactor_me.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        [JsonIgnore]
        public bool IsNew { get; internal set; }

        public Product()
        {
            Id = Guid.NewGuid();
            IsNew = true;
        }

        public Product(Guid id)
        {
			this.Load(id);
        }

        public void DeleteProduct()
        {
			foreach (var option in new ProductOptions(Id).Items)
			{
				option.Delete();
			}

			this.Delete();
        }
    }
}
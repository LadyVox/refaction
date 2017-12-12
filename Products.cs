using System;
using System.Collections.Generic;


namespace refactor_me.Models
{
    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products()
        {
			Items = new List<Product>()
				.Load($"select id from product");
		}

		public Products(string name)
        {
			Items = new List<Product>()
				.Load($"select id from product where lower(name) like '%{name.ToLower()}%'");
        }
    }
}
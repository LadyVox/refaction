using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        [Route]
        [HttpGet]
        public Products GetAll()
        {
            return new Products();
        }

        [Route]
        [HttpGet]
        public Products SearchByName(string name)
        {
            return new Products(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            return new Product(id);
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            product.Save();
        }

		[Route("{id}")]
		[HttpPut]
		public void Update(Guid id, Product product)
		{
			var orig = new Product(id);
			if (orig.IsNew) { return; }

			orig.Name = product.Name;
			orig.Description = product.Description;
			orig.Price = product.Price;
			orig.DeliveryPrice = product.DeliveryPrice;
			orig.Save();
		}

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            var product = new Product(id);
            product.DeleteProduct();
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            return new ProductOptions(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            return new ProductOption(id);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            option.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
			var orig = new ProductOption(id);
			if(orig.IsNew) { return; }

			orig.Name = option.Name;
            orig.Description = option.Description;
			orig.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            var opt = new ProductOption(id);
            opt.Delete();
        }
    }
}

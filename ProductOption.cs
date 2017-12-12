using System;
using Newtonsoft.Json;


namespace refactor_me.Models
{
	public class ProductOption
	{
		public Guid Id { get; set; }

		public Guid ProductId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		[JsonIgnore]
		public bool IsNew { get; internal set; }

		public ProductOption()
		{
			Id = Guid.NewGuid();
			IsNew = true;
		}

		public ProductOption(Guid id)
		{
			this.Load(id);
		}
	}
}
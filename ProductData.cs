using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace refactor_me.Models
{
	public static class ProductData
	{
		internal static void Load(this Product item, Guid id)
		{
			using (var conn = Helpers.NewConnection())
			{
				using (var cmd = new SqlCommand($"select * from product where id = '{id}'", conn))
				{
					conn.Open();

					var rdr = cmd.ExecuteReader();
					if (!rdr.Read())
					{
						item.IsNew = true;
						item.Id = id;
						item.Name = "*** We were unable to locate a product with this Id ***";
						item.Description = null;
						item.Price = new decimal();
						item.DeliveryPrice = new decimal();
						return;
					}

					item.IsNew = false;
					item.Id = Guid.Parse(rdr["Id"].ToString());
					item.Name = rdr["Name"].ToString();
					item.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
					item.Price = decimal.Parse(rdr["Price"].ToString());
					item.DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString());
				}
			}
		}

		internal static List<Product> Load(this List<Product> items, string command)
		{
			using (var conn = Helpers.NewConnection())
			{
				using (var cmd = new SqlCommand(command, conn))
				{
					conn.Open();

					var rdr = cmd.ExecuteReader();
					while (rdr.Read())
					{
						var id = Guid.Parse(rdr["id"].ToString());
						items.Add(new Product(id));
					}
				}
			}

			return items;
		}

		internal static void Save(this Product item)
		{
			using (var conn = Helpers.NewConnection())
			{
				using (var cmd = item.IsNew ?
					new SqlCommand($"insert into product (id, name, description, price, deliveryprice) values ('{item.Id}', '{item.Name}', '{item.Description}', {item.Price}, {item.DeliveryPrice})", conn) :
					new SqlCommand($"update product set name = '{item.Name}', description = '{item.Description}', price = {item.Price}, deliveryprice = {item.DeliveryPrice} where id = '{item.Id}'", conn))
				{

					conn.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}

		internal static void Delete(this Product item)
		{
			using (var conn = Helpers.NewConnection())
			{
				conn.Open();
				using (var cmd = new SqlCommand($"delete from product where id = '{item.Id}'", conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
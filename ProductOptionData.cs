using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace refactor_me.Models
{
	internal static class ProductOptionData
	{
		internal static List<ProductOption> Load(this List<ProductOption> items, string command)
		{
			var conn = Helpers.NewConnection();
			using (var cmd = new SqlCommand(command, conn))
			{
				conn.Open();

				var rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					var id = Guid.Parse(rdr["id"].ToString());
					items.Add(new ProductOption(id));
				}
			}

			return items;
		}

		internal static void Load(this ProductOption item, Guid id)
		{
			var conn = Helpers.NewConnection();
			using (var cmd = new SqlCommand($"select * from productoption where id = '{id}'", conn))
			{
				conn.Open();

				var rdr = cmd.ExecuteReader();
				if (!rdr.Read())
				{
					item.IsNew = true;
					item.Id = id;
					item.ProductId = Guid.Empty;
					item.Name = "*** We were unable to locate a product option with this Id ***";
					item.Description = null;
					return;
				}

				item.IsNew = false;
				item.Id = Guid.Parse(rdr["Id"].ToString());
				item.ProductId = Guid.Parse(rdr["ProductId"].ToString());
				item.Name = rdr["Name"].ToString();
				item.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
			}
		}

		internal static void Save(this ProductOption item)
		{
			using (var conn = Helpers.NewConnection())
			{
				using (var cmd = item.IsNew ?
					new SqlCommand($"insert into productoption (id, productid, name, description) values ('{item.Id}', '{item.ProductId}', '{item.Name}', '{item.Description}')", conn) :
					new SqlCommand($"update productoption set name = '{item.Name}', description = '{item.Description}' where id = '{item.Id}'", conn))
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}

		internal static void Delete(this ProductOption item)
		{
			using (var conn = Helpers.NewConnection())
			{
				conn.Open();
				var cmd = new SqlCommand($"delete from productoption where id = '{item.Id}'", conn);
				cmd.ExecuteReader();
			}
		}
	}
}
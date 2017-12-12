using System.Data.SqlClient;
using System.Web;

namespace refactor_me.Models
{
    public class Helpers
    {
        public static SqlConnection NewConnection()
        {
            var connstr = string.Format(
				Properties.Settings.Default.RefactorMeConnectionString, 
				HttpContext.Current.Server.MapPath("~/App_Data"));

            return new SqlConnection(connstr);
        }
    }
}
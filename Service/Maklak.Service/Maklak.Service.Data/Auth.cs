using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Maklak.Service.Data.Helpers;

namespace Maklak.Service.Data
{
	public static class Auth
	{
		public static bool RegisterUser(string userName, string userPassword) 
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_RegisterUser");

			command.AddInParameter("@UserName", DbType.String, 250, userName);
			command.AddInParameter("@UserPassword", DbType.String, 50, userPassword);			

			return SqlHelper.ExecuteScalar(command) != DBNull.Value;
		}

		public static bool GetUser(string userName, string userPassword)
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_GetUser");

			command.AddInParameter("@UserName", DbType.String, 250, userName);
			command.AddInParameter("@UserPassword", DbType.String, 50, userPassword);

			return SqlHelper.ExecuteScalar(command) != DBNull.Value;
		}
	}
}


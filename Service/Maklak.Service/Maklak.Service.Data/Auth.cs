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

			object userIdObject = SqlHelper.ExecuteScalar(command);

			if (userIdObject == null)
				return false;

			int userId = userIdObject != DBNull.Value ? Convert.ToInt32(userIdObject) : 0;

			return userId != 0;
		}

		public static bool GetUser(string userName, string userPassword)
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_GetUser");

			command.AddInParameter("@UserName", DbType.String, 250, userName);
			command.AddInParameter("@UserPassword", DbType.String, 50, userPassword);

			object userIdObject = SqlHelper.ExecuteScalar(command);

			if (userIdObject == null)
				return false;

			int userId = userIdObject != DBNull.Value ? Convert.ToInt32(userIdObject) : 0;

			return userId != 0;
		}
	}
}


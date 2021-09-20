using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Maklak.Service.Data.Helpers
{
    
    public static class SqlHelper
    {
        public static IDbDataParameter AddInParameter(this IDbCommand command, string paramName, DbType paramType, object paramValue) 
        {
            IDbDataParameter parameter = command.CreateParameter();

            parameter.ParameterName = paramName;
            parameter.DbType = paramType;
            parameter.Value = paramValue;

            command.Parameters.Add(parameter);

            return parameter;
        }
        public static IDbDataParameter AddInParameter(this IDbCommand command, string paramName, DbType paramType, int paramSize, object paramValue)
        {
            IDbDataParameter parameter = command.AddInParameter(paramName, paramType, paramValue);
            parameter.Size = paramSize;
            return parameter;
        }

        // Methods
        private static SqlConnection sqlConnect;
        private static Object lockObject;

        static SqlHelper()
        {
            //string connectStr1 = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;

            //var y = System.Configuration.ConfigurationManager.AppSettings.AllKeys;

            string connectStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString; //@"Data Source=DESKTOP-BV0EAT6\SQLEXPRESS; Initial Catalog=Srv; User ID=sa;Password=saP@ssw0rd;";
            sqlConnect = new SqlConnection(connectStr);
            lockObject = new Object();
        }

        private static IDbConnection Connection { get { return sqlConnect; } }

        public static void Test()
        {
            int i = 0;
        }


        //public static SqlConnection Connect
        //{
        //    get
        //    {
        //        return sqlConnect;
        //    }
        //}

        private static void Open()
        {

            if (sqlConnect == null)
                throw new ArgumentNullException("connection");

            if (sqlConnect.State == ConnectionState.Closed)
                sqlConnect.Open();


        }

        private static void Close()
        {

            if (sqlConnect == null)
                throw new ArgumentNullException("connection");

            if (sqlConnect.State == ConnectionState.Open)
                sqlConnect.Close();


        }

        public static IDbCommand GetDbCommand()        
        {
            IDbCommand command = SqlHelper.Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public static IDbCommand GetDbCommand(string spName)
        {
            IDbCommand command = SqlHelper.GetDbCommand();
            command.CommandText = spName;
            return command;
        }

        public static object ExecuteScalar(IDbCommand command) 
        {
			object result = null;

			lock (lockObject)
			{
				try
				{
					SqlHelper.Open();

					command.Prepare();

					result = command.ExecuteScalar();
				}
				finally
				{
					SqlHelper.Close();
				}				
			}

            return result;
        }

		public static void FillDataTable(IDbCommand command, DataTable table) 
		{		

			lock (lockObject)
			{
				try
				{
					SqlHelper.Open();

					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							DataRow row = table.NewRow();

							foreach (DataColumn column in table.Columns)
							{
								row[column.ColumnName] = reader[column.ColumnName];
							}

							table.Rows.Add(row);
						}

						table.AcceptChanges();
					}
				}
				finally 
				{
					SqlHelper.Close();
				}
				
			}			
		}

		//private IConvertible GetValue() 
		//{

		//}

		//public static T ExecuteDataset<T>(IDbCommand command) where T : DataSet, new()
		//{
		//	T dataSet = new T();

		//	lock (lockObject)
		//	{

		//		try
		//		{
		//			//PrepareCommand(command, commandType, commandText, commandParameters);

		//			using (SqlDataAdapter adapter = new SqlDataAdapter((SqlCommand)command))
		//			{
		//				adapter.Fill(dataSet);
		//				//command.Parameters.Clear();
		//			}
		//		}
		//		finally
		//		{
		//			Close();
		//		}
		//	}

		//	return dataSet;
		//}

		//public static T ExecuteDataset<T>(IDbCommand command) where T: DataSet
		//{
		//	T dataSet = new T();

		//	lock (lockObject)
		//	{

		//		try
		//		{
		//			//PrepareCommand(command, commandType, commandText, commandParameters);

		//			using (SqlDataAdapter adapter = new SqlDataAdapter(command))
		//			{
		//				adapter.Fill(dataSet);
		//				//command.Parameters.Clear();
		//			}
		//		}
		//		finally
		//		{
		//			Close();
		//		}
		//	}

		//	return dataSet;
		//}

		//private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
		//{
		//    if ((commandParameters != null) && (parameterValues != null))
		//    {
		//        if (commandParameters.Length != parameterValues.Length)
		//        {
		//            throw new ArgumentException("Parameter count does not match Parameter Value count.");
		//        }
		//        int index = 0;
		//        int length = commandParameters.Length;
		//        while (index < length)
		//        {
		//            if (parameterValues[index] is IDbDataParameter)
		//            {
		//                IDbDataParameter parameter = (IDbDataParameter)parameterValues[index];
		//                if (parameter.Value == null)
		//                {
		//                    commandParameters[index].Value = DBNull.Value;
		//                }
		//                else
		//                {
		//                    commandParameters[index].Value = parameter.Value;
		//                }
		//            }
		//            else if (parameterValues[index] == null)
		//            {
		//                commandParameters[index].Value = DBNull.Value;
		//            }
		//            else
		//            {
		//                commandParameters[index].Value = parameterValues[index];
		//            }
		//            index++;
		//        }
		//    }
		//}

		//private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
		//{
		//    if (command == null)
		//    {
		//        throw new ArgumentNullException("command");
		//    }
		//    if (commandParameters != null)
		//    {
		//        foreach (SqlParameter parameter in commandParameters)
		//        {
		//            if (parameter != null)
		//            {
		//                if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
		//                {
		//                    parameter.Value = DBNull.Value;
		//                }
		//                command.Parameters.Add(parameter);
		//            }
		//        }
		//    }
		//}



		//public static DataSet ExecuteDataset(CommandType commandType, string commandText)
		//{
		//    // used
		//    return ExecuteDataset(commandType, commandText, null);
		//}

		//public static DataSet ExecuteDataset(string spName, params SqlParameter[] parameterValues)
		//{

		//    if ((spName == null) || (spName.Length == 0))
		//    {
		//        throw new ArgumentNullException("spName");
		//    }
		//    if ((parameterValues != null) && (parameterValues.Length > 0))
		//    {
		//        //Open();
		//        //SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnect, spName);
		//        //AssignParameterValues(spParameterSet, parameterValues);
		//        //Close();
		//        return ExecuteDataset(/*connectionString,*/ CommandType.StoredProcedure, spName, /*spParameterSet*/parameterValues);
		//    }
		//    return ExecuteDataset(/*connectionString, */CommandType.StoredProcedure, spName);
		//}

		//public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		//{
		//    DataSet dataSet = new DataSet();

		//    lock (lockObject)
		//    {
		//        SqlCommand command = new SqlCommand();
		//        try
		//        {
		//            PrepareCommand(command, commandType, commandText, commandParameters);

		//            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
		//            {
		//                adapter.Fill(dataSet);
		//                command.Parameters.Clear();
		//            }
		//        }
		//        finally
		//        {
		//            Close();
		//        }
		//    }

		//    return dataSet;
		//}



		//public static int ExecuteNonQuery(string spName, params SqlParameter[] parameterValues)
		//{


		//    if ((spName == null) || (spName.Length == 0))
		//    {
		//        throw new ArgumentNullException("spName");
		//    }
		//    if ((parameterValues != null) && (parameterValues.Length > 0))
		//    {
		//        //SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnect, spName);
		//        //AssignParameterValues(spParameterSet, parameterValues);
		//        return ExecuteNonQuery(CommandType.StoredProcedure, spName, /*spParameterSet*/parameterValues);
		//    }
		//    return ExecuteNonQuery(CommandType.StoredProcedure, spName);
		//}



		//public static int ExecuteNonQuery(CommandType commandType, string commandText)
		//{

		//    return ExecuteNonQuery(commandType, commandText, null);
		//}



		//public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		//{
		//    int num = 0;
		//    lock (lockObject)
		//    {
		//        SqlCommand command = new SqlCommand();
		//        try
		//        {
		//            PrepareCommand(command, commandType, commandText, commandParameters);
		//            num = command.ExecuteNonQuery();
		//            command.Parameters.Clear();
		//        }
		//        finally
		//        {

		//            Close();
		//        }
		//    }

		//    return num;
		//}



		//public static IDataReader ExecuteReader(CommandType commandType, string commandText)
		//{

		//    return ExecuteReader(commandType, commandText, null);
		//}



		//public static IDataReader ExecuteReader(string spName, params SqlParameter[] parameterValues)
		//{

		//    if ((spName == null) || (spName.Length == 0))
		//    {
		//        throw new ArgumentNullException("spName");
		//    }
		//    if ((parameterValues != null) && (parameterValues.Length > 0))
		//    {
		//        //SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnect, spName);
		//        //AssignParameterValues(spParameterSet, parameterValues);
		//        return ExecuteReader(CommandType.StoredProcedure, spName, /*spParameterSet*/ parameterValues);
		//    }
		//    return ExecuteReader(CommandType.StoredProcedure, spName);
		//}



		//public static IDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		//{



		//    IDataReader dataReader = null;

		//    lock (lockObject)
		//    {

		//        using (IDataReader reader = ExecuteReader(commandType, commandText, commandParameters, SqlConnectionOwnership.Internal))
		//        {
		//            dataReader = convertDataReader(reader);
		//        }

		//        Close();
		//    }


		//    return dataReader;
		//}

		//// Input - SqlDataReader
		//// Output - DataTableReader
		//private static IDataReader convertDataReader(IDataReader reader)
		//{
		//    DataSet ds = new DataSet();

		//    do
		//    {
		//        DataTable data = new DataTable();

		//        fillTable(data, reader);

		//        ds.Tables.Add(data);
		//    }
		//    while (reader.NextResult());

		//    return ds.CreateDataReader();
		//}
		//// DataTable method Load(IDataReader ...) close the reader. So imposible to obtain NextResult when multiple tables returns.
		//// fillTable(...) fills DataTable whithout closing reader.
		//private static void fillTable(DataTable data, IDataReader reader)
		//{
		//    if (reader.IsClosed)
		//        return;

		//    for (int i = 0; i < reader.FieldCount; i++)
		//    {
		//        string fieldName = reader.GetName(i);
		//        // skip duplicate columns
		//        if (data.Columns.Contains(fieldName))
		//            continue;

		//        Type fieldType = reader.GetFieldType(i);


		//        DataColumn column = new DataColumn(fieldName, fieldType);
		//        data.Columns.Add(column);

		//    }

		//    while (reader.Read())
		//    {
		//        DataRow row = data.NewRow();

		//        foreach (DataColumn col in data.Columns)
		//            row[col.ColumnName] = reader.GetValue(reader.GetOrdinal(col.ColumnName));

		//        data.Rows.Add(row);
		//    }

		//}

		//private static SqlDataReader ExecuteReader(CommandType commandType, string commandText, SqlParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
		//{
		//    SqlDataReader reader = null;


		//    SqlCommand command = new SqlCommand();
		//    try
		//    {
		//        PrepareCommand(command, commandType, commandText, commandParameters);

		//        CommandBehavior comBehavior = connectionOwnership == SqlConnectionOwnership.External ? CommandBehavior.Default : CommandBehavior.CloseConnection;
		//        reader = command.ExecuteReader(comBehavior);
		//    }
		//    catch
		//    {
		//        Close();
		//        throw;
		//    }
		//    bool clear = true;

		//    foreach (SqlParameter parameter in command.Parameters)
		//    {
		//        if (parameter.Direction != ParameterDirection.Input)
		//        {
		//            clear = false;
		//            break;
		//        }
		//    }

		//    if (clear)
		//        command.Parameters.Clear();


		//    return reader;
		//}



		//public static object ExecuteScalar(CommandType commandType, string commandText)
		//{
		//    return ExecuteScalar(commandType, commandText, null);
		//}

		//public static object ExecuteScalar(string spName, params SqlParameter[] parameterValues)
		//{

		//    if ((spName == null) || (spName.Length == 0))
		//    {
		//        throw new ArgumentNullException("spName");
		//    }
		//    if ((parameterValues != null) && (parameterValues.Length > 0))
		//    {
		//        //SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnect, spName);
		//        //AssignParameterValues(spParameterSet, parameterValues);
		//        return ExecuteScalar(CommandType.StoredProcedure, spName, /*spParameterSet*/parameterValues);
		//    }
		//    return ExecuteScalar(CommandType.StoredProcedure, spName);
		//}



		//public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		//{

		//    SqlCommand command = new SqlCommand();
		//    object retObject = null;

		//    lock (lockObject)
		//    {
		//        try
		//        {
		//            PrepareCommand(command, commandType, commandText, commandParameters);
		//            retObject = command.ExecuteScalar();
		//            command.Parameters.Clear();
		//        }
		//        finally
		//        {
		//            Close();
		//        }


		//    }

		//    return retObject;
		//}



		//public static int GetSize(string csTableName, string csColumnName, int iDefault)
		//{
		//    int num = iDefault;
		//    string cmdText = "select CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS \r\n                WHERE TABLE_NAME = @TABLE_NAME AND COLUMN_NAME = @COLUMN_NAME";
		//    try
		//    {


		//        Open();

		//        SqlCommand command = new SqlCommand(cmdText, sqlConnect);
		//        //command.Parameters.Add("@TABLE_NAME", csTableName);
		//        command.Parameters.AddWithValue("@TABLE_NAME", csTableName);
		//        //command.Parameters.Add("@COLUMN_NAME", csColumnName);
		//        command.Parameters.AddWithValue("@COLUMN_NAME", csColumnName);

		//        using (SqlDataReader reader = command.ExecuteReader())
		//        {
		//            if (reader.Read())
		//            {
		//                object obj2 = reader.GetValue(0);
		//                if (obj2 != DBNull.Value)
		//                {
		//                    num = Convert.ToInt32(obj2);
		//                }
		//            }
		//        }

		//        Close();


		//    }
		//    catch (Exception)
		//    {
		//        Close();
		//    }
		//    return num;
		//}

		//private static void PrepareCommand(SqlCommand command, CommandType commandType, string commandText, SqlParameter[] commandParameters)
		//{
		//    if (command == null)
		//    {
		//        throw new ArgumentNullException("command");
		//    }
		//    if ((commandText == null) || (commandText.Length == 0))
		//    {
		//        throw new ArgumentNullException("commandText");
		//    }

		//    Open();



		//    string timeoutSttings = System.Configuration.ConfigurationManager.AppSettings["timeout"];
		//    int timeout = string.IsNullOrEmpty(timeoutSttings) ? 30 : Convert.ToInt32(timeoutSttings);
		//    command.Connection = sqlConnect;
		//    command.CommandText = commandText;
		//    command.CommandTimeout = timeout;

		//    command.CommandType = commandType;
		//    if (commandParameters != null)
		//    {
		//        AttachParameters(command, commandParameters);
		//    }
		//}



		//// Nested Types
		//private enum SqlConnectionOwnership
		//{
		//    Internal,
		//    External
		//}

	}//
}

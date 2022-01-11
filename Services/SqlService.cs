using Dapper;
using InvoiceApi.Constants;
using InvoiceApi.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public class SqlService : ISqlService
    {
        #region  Variable Declaration

        private IConfiguration _configuration;
        private IDbConnection dbConnection => new SqlConnection(Utility.GetConnectionString
                ("EformsBuddyApiDB"));
        #endregion

        public SqlService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Execute Insert Update using Stored Procedure
        /// <summary>
        /// Execute Insert Update using Stored Procedure
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="inputParameter"></param>
        /// <returns></returns>

        public int Execute(string storedProcedure, object inputParameter)
        {
            return dbConnection.Execute(storedProcedure, param: inputParameter, commandType: CommandType.StoredProcedure);
        }
        #endregion

        #region Execute Insert Update using Query
        /// <summary>
        ///  Execute Insert Update using Query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        public int Execute_Query(string query, object inputParameter)
        {
            return dbConnection.Execute(query, param: inputParameter, commandType: CommandType.Text);
        }
        #endregion

        #region  Retrive Record  Based on condition using Stored Procedure
        /// <summary>
        /// Retrive Record  Based on condition using Stored Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        public IEnumerable<T> GetDataProcedure<T>(string storedProcedure, object inputParameter)
        {
            return dbConnection.Query<T>(storedProcedure, param: inputParameter, commandType: CommandType.StoredProcedure);
        }
        #endregion

        #region Retrive Record using query with parameter
        /// <summary>
        ///  Retrive Record  Based on condition using query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        public IEnumerable<T> GetData_Query<T>(string query, object inputParameter)
        {
            return dbConnection.Query<T>(query, param: inputParameter, commandType: CommandType.Text);
        }
        #endregion

        #region Retrive Record using query without parameter
        /// <summary>
        /// Retrive Record  Based on condition using query without parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> GetData_Query<T>(string query)
        {
            return dbConnection.Query<T>(query, commandType: CommandType.Text);
        }
        #endregion

        #region GetRecord using  query with parameter
        /// <summary>
        /// GetRecord with query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        public IEnumerable<T> GetDataTableQuery<T>(string query, object inputParameter)
        {
            return dbConnection.Query<T>(query, param: inputParameter, commandType: CommandType.Text);
        }

        #endregion

        #region GetMultipleResultSet
        /// <summary>
        /// Select Multiple
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="InputParameter"></param>
        /// <returns></returns>
        public SqlMapper.GridReader GetMultipleResultSet(string storedProcedure, object InputParameter)
        {
            return dbConnection.QueryMultiple(storedProcedure, param: InputParameter, commandType: CommandType.StoredProcedure);
        }

        #endregion]

        #region GetDataTable
        /// <summary>
        /// GetDataTable
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="InputParameter"></param>
        /// <returns></returns>
        public IDataReader GetDataTable(string storeProcedure, object inputParameter)
        {
            return dbConnection.ExecuteReader(storeProcedure, param: inputParameter, commandType: CommandType.StoredProcedure);
        }
        #endregion


        #region ExecuteScalar Query Execution Method


        #region ExecuteScalar using Query  with parameter
        public object ExecuteScalar_Query(string query, object inputParamter)
        {
            return dbConnection.ExecuteScalar<object>(query, param: inputParamter, commandType: CommandType.Text);
        }
        #endregion

        #region ExecuteScalar using StoreProcedure  with parameter
        public object ExecuteScalar_SP(string storedProcedure, object inputParamter)
        {
            return dbConnection.ExecuteScalar(storedProcedure, param: inputParamter, commandType: CommandType.StoredProcedure);
        }
        #endregion

        #region ExecuteScalar using Query  without parameter
        public object ExecuteScalar_Query(string query)
        {
            return dbConnection.ExecuteScalar<object>(query, commandType: CommandType.Text);
        }
        #endregion

        #region ExecuteScalar using StoreProcedure  without parameter
        public object ExecuteScalar_SP(string storedProcedure)
        {
            return dbConnection.ExecuteScalar<object>(storedProcedure, commandType: CommandType.StoredProcedure);
        }
       

        public async Task<T>  GetSingleExecuteQueryasync<T>(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var dbConn=dbConnection)
            {
               return await dbConn.QueryFirstOrDefaultAsync<T>(query, param, commandType: commandType);
            }
           
        }

        public async Task<IEnumerable<T>> GetListExecuteQueryasync<T>(string sp, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var dbConn = dbConnection)
            {
              var results =   await dbConn.QueryAsync<T>(sp, param, commandType: commandType);
                return results.ToList();
            }
        }

        SqlMapper.GridReader ISqlService.GetMultipleResultSet(string storedProcedure, object InputParameter)
        {
            throw new NotImplementedException();
        }


        #endregion

        #endregion
    }
}

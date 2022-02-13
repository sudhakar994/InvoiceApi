using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
    public interface ISqlService
    {
        //Async Dapper Methods  

        #region Query Based

        Task<T> GetSingleExecuteQueryasync<T>(string query, object parameters, CommandType commandType = CommandType.Text);
        Task<T> GetSingleExecuteSPasync<T>(string storedProcedure, object parameters, CommandType commandType = CommandType.StoredProcedure);
        Task<List<T>> GetListExecuteQueryasync<T>(string query, object param = null, CommandType commandType = CommandType.Text);
        #endregion

        #region Stored Procedure Based
        Task<int> ExecuteSP(string storedProcedure, object parameters, CommandType commandType = CommandType.StoredProcedure);
        Task<List<T>> SPGetListExecuteQueryasync<T>(string storedProcedure, object param = null, CommandType commandType = CommandType.StoredProcedure);
        #endregion

    }
}

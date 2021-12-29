using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
    public interface ISqlService
    {
        IEnumerable<T> GetDataProcedure<T>(string storedProcedure, object inputParameter);
        int Execute(string storedProcedure, object inputParameter);
        SqlMapper.GridReader GetMultipleResultSet(string storedProcedure, object InputParameter);
        IEnumerable<T> GetDataTableQuery<T>(string query, object inputParameter);
        IDataReader GetDataTable(string storeProcedure, object inputParameter);
        IEnumerable<T> GetData_Query<T>(string query, object inputParameter);
        IEnumerable<T> GetData_Query<T>(string query);
    }
}

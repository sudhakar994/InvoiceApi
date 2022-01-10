using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace InvoiceApi.Repository
{
    public class UserRepository: IUserReposiotry
    {
        private readonly ISqlService _sqlService;

        public UserRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }
        public async Task<User> Register(User user)
        {
            var query = @"";
            var param = new { };

            return await _sqlService.ExecuteQueryasync(query, param);
        }
    }
}

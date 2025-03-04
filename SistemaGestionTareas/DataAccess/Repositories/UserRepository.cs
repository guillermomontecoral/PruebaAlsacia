using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Inyeccion de dependencias
        private readonly TaskDbContext _taskDbContext;

        public UserRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }
        #endregion
        public async Task<int> Validate(string email, string password)
        {
            var user = await _taskDbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("The user is not registered.");

            if (password != user.Password)
            {
                throw new Exception("The login details are incorrect.");
            }

            return user.Id;
        }
    }
}

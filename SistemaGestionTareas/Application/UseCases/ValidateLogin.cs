using Application.UseCases.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ValidateLogin : IValidateLogin
    {
        #region Dependency Injection
        private readonly IUserRepository _userRepository;

        public ValidateLogin(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion
        public Task<int> Validate(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("El email es requerido.");

            if (string.IsNullOrEmpty(password))
                throw new Exception("La contraseña es requerida.");

            return _userRepository.Validate(email, password);
        }
    }
}

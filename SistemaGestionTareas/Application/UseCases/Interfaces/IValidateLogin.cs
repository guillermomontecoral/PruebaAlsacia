using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Interfaces
{
    public interface IValidateLogin
    {
        Task<int> Validate(string email, string password);
    }
}

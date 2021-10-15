using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.SignUp
{
    public interface IAddUserBase
    {
        Task<int> AddUserBaseAsync(UserModel User);
    }
}

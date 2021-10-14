using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.SignUp
{
    public interface IAddUserBase
    {
        UserModel User { get; set; }
        Task<int> AddUserBaseAsync(UserModel entity);
    }
}

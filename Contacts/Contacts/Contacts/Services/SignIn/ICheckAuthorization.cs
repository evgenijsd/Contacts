using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Services.SignIn
{
    public interface ICheckAuthorization
    {
        int UserId { get; set; }
    }
}

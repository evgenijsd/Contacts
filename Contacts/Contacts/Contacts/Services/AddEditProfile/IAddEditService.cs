using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.AddEditProfile
{
    public interface IAddEditService
    {
        Task<int> AddEditExecute(string choise, ContactModel contact);
        Task<string> Photo();
    }
}

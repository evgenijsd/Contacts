using Contacts.Models;
using Contacts.Services.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using static Contacts.Services.Settings.SettingsType;

namespace Contacts.Services.MainList
{
    public interface IMainListService
    {
        Task DeleteContactAsync(ObservableCollection<ContactView> collecti, object contactObj);
        Task<ObservableCollection<ContactView>> GetCollectionAsync(int userId);
        ObservableCollection<ContactView> SortCollection(ObservableCollection<ContactView> collection, SettingsType settings);
    }
}

using Contacts.Models;
using Contacts.Services.Settings;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Contacts.Services.MainList
{
    public interface IMainListService
    {
        Task DeleteContactAsync(ObservableCollection<ContactView> collecti, object contactObj);
        Task<ObservableCollection<ContactView>> GetCollectionAsync(int userId);
        ObservableCollection<ContactView> SortCollection(ObservableCollection<ContactView> collection, SortType settings);
    }
}

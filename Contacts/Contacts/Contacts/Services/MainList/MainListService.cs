using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static Contacts.Services.Settings.SettingsType;

namespace Contacts.Services.MainList
{
    public class MainListService : IMainListService
    {
        private IRepository _repository { get; }

        public MainListService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteContactAsync(ObservableCollection<ContactView> collection, object contactObj)
        {
            ContactView contact = contactObj as ContactView;
            ContactModel contactdel = contact.ToContact();
            await _repository.RemoveAsync<ContactModel>(contactdel);
            collection.Remove(contact);
        }

        public ObservableCollection<ContactView> SortCollection(ObservableCollection<ContactView> collection, SettingsType settings)
        {
            switch (settings)
            {
                case SettingsType.SortByName:
                    collection = new ObservableCollection<ContactView>(collection.OrderBy(x => x.Name)); break;
                case SettingsType.SortByNickname:
                    collection = new ObservableCollection<ContactView>(collection.OrderBy(x => x.Nickname)); break;
                case SettingsType.SortByData:
                    collection = new ObservableCollection<ContactView>(collection.OrderBy(x => x.Date)); break;
            }
            return collection;
        }

        public async Task<ObservableCollection<ContactView>> GetCollectionAsync(int userId) =>
            new ObservableCollection<ContactView>((await _repository.GetAsync<ContactModel>(x => x.UserId == userId)).Select(x => x.ToContactView()));
        
    }
}

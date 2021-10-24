using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contacts.Services.Settings
{
    public interface IAllSetting
    {
        int SortSet { get; set; }
        int ThemeSet { get; set; }
        int LangSet { get; set; }
        int ChangeTheme(bool theme);
        string Language(LangType language);
        ObservableCollection<LangModel> GetLanguages();
    }
}

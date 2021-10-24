using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Contacts.Services
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}

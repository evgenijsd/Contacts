using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Models
{
    public class ContactModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ProfileId { get; set; }
        public string Image { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public virtual UserModel UserNavigation { get; set; }
    }
}

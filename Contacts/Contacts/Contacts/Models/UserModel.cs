using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Models
{
    [Table("Users")]
    class UserModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Passsword { get; set; }

        public virtual ICollection<ContactModel> Contscts { get; set; }
    }
}

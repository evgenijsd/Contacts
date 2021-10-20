﻿using SQLite;

namespace Contacts.Models
{
    [Table("Users")]
    public class UserModel : IEntity
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

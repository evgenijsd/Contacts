using SQLite;
using System;

namespace Contacts.Models
{
    public class ContactModel : IEntity
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Image { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}

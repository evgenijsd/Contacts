namespace Contacts.Models
{
    public static class ContactExtension
    {
        public static ContactModel ToContact(this ContactView contact)
        {
            ContactModel n = new ContactModel()
            {
                Id = contact.Id,
                Image = contact.Image,
                UserId = contact.UserId,
                Name = contact.Name,
                Nickname = contact.Nickname,
                Date = contact.Date
            };
            return n;
        }

        public static ContactView ToContactView(this ContactModel contact)
        {
            ContactView n = new ContactView()
            {
                Id = contact.Id,
                Image = contact.Image,
                UserId = contact.UserId,
                Name = contact.Name,
                Nickname = contact.Nickname,
                Date = contact.Date
            };
            return n;
        }
    }
}

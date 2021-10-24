using Contacts.Models;
using Contacts.Services.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Contacts.Services.AddEditProfile
{
    public class AddEditService : IAddEditService
    {
        private IRepository _repository { get; }

        public AddEditService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddEditExecute(string choice, ContactModel contact)
        {
            int result;
            if (choice == "Add Profile")
            {
                result = await _repository.AddAsync(contact);
            }
            else
            {
                result = await _repository.UpdateAsync(contact);
            };
            return result;
        }

        public async Task<string> Photo(ImageChoise choise)
        {
            string result = "user.png";

            switch (choise)
            {
                case ImageChoise.gallery:
                    try
                    {
                        result = (await MediaPicker.PickPhotoAsync()).FullPath;
                    }
                    catch { }
                    break;
                case ImageChoise.camera:
                    try
                    {
                        var photo = await MediaPicker.CapturePhotoAsync();
                        var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                        using (var stream = await photo.OpenReadAsync())
                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);
                        result = photo.FullPath;
                    }
                    catch { }
                    break;
            }
            return result;
        }
    }
}

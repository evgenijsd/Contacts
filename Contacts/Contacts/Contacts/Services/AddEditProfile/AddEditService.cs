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

        public async Task<int> AddEditExecute(ProfileType choise, ContactModel contact)
        {
            int result = 0;
            switch (choise)
            {
                case ProfileType.Add:
                    result = await _repository.AddAsync(contact);
                    break;
                case ProfileType.Edit:
                    result = await _repository.UpdateAsync(contact);
                    break;
                default:
                    break;
            }
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

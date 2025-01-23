using CommunityToolkit.Mvvm.ComponentModel;
using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastImg.Client.Views
{
    public partial class ImageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Byte[]? imageData;

        public async Task<bool> ShowImageAsync(long id)
        {
            var imageResponse = await App.API?.Image.GetImageAsync(id, 0);
            if (!imageResponse.IsSuccessful) return false;

            using var m = new MemoryStream();
            await imageResponse.Content.CopyToAsync(m);
            ImageData = m.ToArray();
            return true;
        }
    }
}

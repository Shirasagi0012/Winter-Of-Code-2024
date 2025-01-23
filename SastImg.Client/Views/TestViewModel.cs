using CommunityToolkit.Mvvm.ComponentModel;
using SastImg.Client.Model;
using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastImg.Client.Views
{
    internal partial class TestViewModel : ObservableObject
    {
        public ObservableCollection<ImageModel> Images { get; } = [];

        [ObservableProperty]
        private ImageModel? selectedImage;

        public async Task<bool> GetAllImagesAsync()
        {
            Images.Clear();
            var imagesRequest = await App.API!.Image.GetImagesAsync(null,null,null);
            if (!imagesRequest.IsSuccessful) return false; // 如果获取失败，返回 false

            foreach (var image in imagesRequest.Content)
            {
                Images.Add(await ImageModel.GetModelAsync(image));
            }

            return true;
        }
    }
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using SastImg.Client.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImg.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestView : Page
    {
        TestViewModel ViewModel = new();

        public TestView()
        {
            this.InitializeComponent();
        }

        private void image_list_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is not ImageModel c) return;
            if (image_list.SelectedItem is not ImageModel s) return;
            if (c != s) return;
            App.Shell.MainFrame.Navigate(typeof(ImageView), c, new DrillInNavigationTransitionInfo());
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.GetAllImagesAsync();
            image_list.SelectedIndex = 0;
        }
    }
}

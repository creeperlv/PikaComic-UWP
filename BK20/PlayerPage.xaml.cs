using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace BK20
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PlayerPage : Page
    {
        public PlayerPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var obj= e.Parameter as object[];
            LoadUrl(obj[0].ToString());

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            mediaElement.Stop();
        }
        private async void LoadUrl(string id)
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                //mediaElement
                string results= await WebClientClass.GetResults_Avnight(new Uri("https://api2.iavnight.com/v2/videos/"+ id));
                JObject jobj = JObject.Parse(results);

                mediaElement.Source = new Uri(jobj["hls"].ToString());
            }
            catch (Exception ex)
            {
                await new MessageDialog("加载失败").ShowAsync();
            }
            finally
            {
                pr_Load.Visibility = Visibility.Collapsed;

            }
        }
      

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;


// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace BK20
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AVNightPage : Page
    {
        public AVNightPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }


        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (sv.VerticalOffset == sv.ScrollableHeight)
            {
                if (!_loading)
                {
                    Load();
                }
            }
        }

        private void ls_items_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageCenter.SendNavigateTo(NavigateMode.Play, typeof(PlayerPage), new object[] { (e.ClickedItem as AvModel).id });
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            _keyword = txt_Search.Text;
            if (_keyword=="")
            {
                return;
            }
            ls_items.ItemsSource = null;
            _loadEnd = false;
            _next = 0;
            Load();
        }

        string _keyword = "";
        int _next = 0;
        bool _loadEnd = false;
        bool _loading = false;
        private async void Load()
        {
            try
            {
             
                if (_loadEnd)
                {
                    messShow.Show("加载完了...",3000);
                    return;
                }
                _loading=true;
                pr_Load.Visibility = Visibility.Visible;
                //txt_Search
                string url = "https://api2.iavnight.com/v2/search?q=" + Uri.EscapeDataString(_keyword);
                if (_next != 0)
                {
                    url += "&next=" + _next;
                }
                string results = await WebClientClass.GetResults_Avnight(new Uri(url));
                AVListModel ls = JsonConvert.DeserializeObject<AVListModel>(results);
                if (ls_items.ItemsSource == null)
                {
                    ls_items.ItemsSource = ls.data;
                }
                else
                {
                    ls.data.ToList().ForEach(x => (ls_items.ItemsSource as ObservableCollection<AvModel>).Add(x));
                }

                if (ls.next == null)
                {
                    _loadEnd = true;
                }
                else
                {
                    _next = ls.next.Value;
                }
            }
            catch (Exception)
            {

                messShow.Show("加载发生错误",3000);
            }
            finally
            {
                _loading = false;
                pr_Load.Visibility = Visibility.Collapsed;
            }
        }


    }
    public class AVListModel
    {
        public ObservableCollection<AvModel> data { get; set; }
        public int? next { get; set; }
    }
    public class AvModel
    {
        public string cover { get; set; }
        public long duration { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public bool pixelated { get; set; }

        public string Pixelated { get {
                if (pixelated)
                {
                    return "骑兵";
                }
                else
                {
                    return "步兵";
                }
            } }


    }


}

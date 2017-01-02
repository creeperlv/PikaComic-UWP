using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace BK20
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FavouritePage : Page
    {
        public FavouritePage()
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
            if (e.NavigationMode == NavigationMode.New)
            {
                pageNum = 1;
                LoadData();
            }
        }

        bool loading = false;
        int pageNum = 1;
        private async void LoadData()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                loading = true;
                string uri = "";
                if (pageNum == 1)
                {
                    ls_items.Items.Clear();
                }

                uri = string.Format("https://picaapi.picacomic.com/users/favourite?page={0}&rnd={1}", pageNum,new Random().Next(1,9999));
                string results = await WebClientClass.GetResults(new Uri(uri));
                ComicsModel lists = JsonConvert.DeserializeObject<ComicsModel>(results);
                if (lists.code == 200)
                {
                    if (lists.data.comics.docs.Count != 0)
                    {
                        lists.data.comics.docs.ForEach(x => ls_items.Items.Add(x));
                        pageNum++;
                    }
                    else
                    {
                        messShow.Show("沒有更多了", 2000);
                    }
                }
                else
                {
                    messShow.Show(lists.message, 2000);

                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147012867)
                {
                    messShow.Show("檢查你的網絡連接！", 3000);
                }
                else
                {
                    messShow.Show("讀取信息失敗了，挂個VPN試試？", 3000);
                }

            }
            finally
            {
                pr_Load.Visibility = Visibility.Collapsed;
                loading = false;
            }

        }

        private void sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (sv.VerticalOffset == sv.ScrollableHeight)
            {
                if (!loading)
                {
                    LoadData();

                }
            }
        }

        private void ls_items_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageCenter.SendNavigateTo(NavigateMode.Info, typeof(InfoPage), new object[] { (e.ClickedItem as ComicsModel)._id });
        }
    }
}

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
    public sealed partial class RankPage : Page
    {
        public RankPage()
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

            if (e.NavigationMode== NavigationMode.New)
            {
                cb_Time.SelectedIndex = 0;

            }
          
        }
        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivot.SelectedIndex==1&&ls_users.ItemsSource==null)
            {
                LoadRankUser();
            }
        }

        private void cb_Time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadRank();
        }
        private async void LoadRank()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
               
                string uri = "";
                if (cb_Time.SelectedIndex==0)
                {
                    uri = string.Format(" https://picaapi.picacomic.com/comics/leaderboard?tt=H24&ct=VC");
                }
                if (cb_Time.SelectedIndex == 1)
                {
                    uri = string.Format(" https://picaapi.picacomic.com/comics/leaderboard?tt=D7&ct=VC");
                }
                if (cb_Time.SelectedIndex == 2)
                {
                    uri = string.Format(" https://picaapi.picacomic.com/comics/leaderboard?tt=D30&ct=VC");
                }
                string results = await WebClientClass.GetResults(new Uri(uri));
                RandomModel lists = JsonConvert.DeserializeObject<RandomModel>(results);
                if (lists.code == 200)
                {
                    if (lists.data.comics.Count != 0)
                    {
                        ls_items.ItemsSource = lists.data.comics;
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
            }

        }
        private async void LoadRankUser()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;

                string uri = "";
                    uri = string.Format("https://picaapi.picacomic.com/comics/knight-leaderboard");

                string results = await WebClientClass.GetResults(new Uri(uri));
                UserRankModel lists = JsonConvert.DeserializeObject<UserRankModel>(results);
                if (lists.code == 200)
                {
                    if (lists.data.users.Count != 0)
                    {
                        ls_users.ItemsSource = lists.data.users;
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
            }

        }
        private void ls_items_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageCenter.SendNavigateTo(NavigateMode.Info, typeof(InfoPage), new object[] { (e.ClickedItem as RandomModel)._id });
        }

        private void ls_users_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ColumnPage), new object[] { (e.ClickedItem as UserRankModel)._id + "," + (e.ClickedItem as UserRankModel).name, false, true });
        }
    }

    public class UserRankModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public UserRankModel data { get; set; }
        public List<UserRankModel> users { get; set; }
        public int total { get; set; }
        public int pages { get; set; }

        public string _id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
  public int comicsUploaded { get; set; }

        public UserRankModel avatar { get; set; }
        public string originalName { get; set; }
        public string path { get; set; }
        public string fileServer { get; set; }
        public string image
        {
            get
            {
                return fileServer + "/static/" + path;
            }
        }
       

    }

}

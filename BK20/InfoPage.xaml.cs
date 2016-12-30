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
    public sealed partial class InfoPage : Page
    {
        public InfoPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _id = (e.Parameter as object[])[0].ToString();
            LoadData();
        }
        string _id = "";
        private async void LoadData()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                
                string uri = "";
               
                uri = "https://picaapi.picacomic.com/comics/" + _id;

                string results = await WebClientClass.GetResults(new Uri(uri));
                InfoModel info = JsonConvert.DeserializeObject<InfoModel>(results);
                if (info.code == 200)
                {
                    this.DataContext = info.data.comic;
                }
                else
                {
                    messShow.Show(info.message, 2000);

                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147012867)
                {
                    messShow.Show("检查你的网络连接！", 3000);
                }
                else
                {
                    messShow.Show("读取信息失败了，挂个VPN试试？", 3000);
                }

            }
            finally
            {
                pr_Load.Visibility = Visibility.Collapsed;
              
            }
        }

        private  void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }

    public class InfoModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public InfoModel data { get; set; }
        public InfoModel comic { get; set; }

        public InfoModel _creator { get; set; }
        public string gender { get; set; }
        public string name { get; set; }
        public bool verified { get; set; }
        public string exp { get; set; }
        public string level { get; set; }

        public string title { get; set; }
        public string description { get; set; }

       
        public string _id { get; set; }
        public string author { get; set; }

        public int epsCount { get; set; }
        public int pagesCount { get; set; }
        public bool finished { get; set; }
        public int likesCount { get; set; }
        public int viewsCount { get; set; }
        public bool isFavourite { get; set; }
        public bool isLiked { get; set; }
        public int commentsCount { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }

        public List<string> categories { get; set; }
        public InfoModel thumb { get; set; }
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
        public string cats
        {
            get
            {
                string a = "";
                categories.ForEach(x => a += x + ",");
                if (a.Length != 0)
                {
                    a = a.Remove(a.Length - 1);
                }
                return a;
            }
        }

    }

}

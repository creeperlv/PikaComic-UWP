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
    public sealed partial class ColumnPage : Page
    {
        public ColumnPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode== NavigationMode.New)
            {
                txt_Header.Text = ((object[])e.Parameter)[0].ToString();
                name = ((object[])e.Parameter)[0].ToString();
                isSearch = (bool)((object[])e.Parameter)[1];
                isUser = (bool)((object[])e.Parameter)[2];
                pageNum = 1;
                if (isSearch)
                {
                    LoadSearch();
                }
                else
                {
                    if (isUser)
                    {
                        name = ((object[])e.Parameter)[0].ToString().Split(',')[0];
                        txt_Header.Text = ((object[])e.Parameter)[0].ToString().Split(',')[1];
                        LoadUser();
                    }
                    else
                    {
                        if (name == "隨機本子")
                        {
                            LoadRandom();
                        }
                        else
                        {
                            LoadData();
                        }
                    }
                    
                }
            }
          
            
        }
        string name = "";
        bool loading = false;
        bool isSearch = false;
        bool isUser = false;
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
                switch (name)
                {
                    case "隨機本子":
                        uri = "https://picaapi.picacomic.com/comics/random?page=" + pageNum;
                        break;
                    case "最近更新":
                        uri = "https://picaapi.picacomic.com/comics?page=" + pageNum;
                        break;
                    default:
                        uri = string.Format("https://picaapi.picacomic.com/comics?page={1}&c={0}&s=ua", Uri.EscapeDataString(name), pageNum);
                        break;
                }
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
        private async void LoadSearch()
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

                uri = string.Format("https://picaapi.picacomic.com/comics/search?page={1}&q={0}", Uri.EscapeDataString(name), pageNum);

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
        private async void LoadUser()
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

                uri = string.Format("https://picaapi.picacomic.com/comics?page={1}&ca={0}", name, pageNum);

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
        private async void LoadRandom()
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

                uri = "https://picaapi.picacomic.com/comics/random?page=" + pageNum;

                string results = await WebClientClass.GetResults(new Uri(uri));
                RandomModel lists = JsonConvert.DeserializeObject<RandomModel>(results);
                if (lists.code == 200)
                {
                    if (lists.data.comics.Count != 0)
                    {
                        lists.data.comics.ForEach(x => ls_items.Items.Add(x));
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
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (sv.VerticalOffset == sv.ScrollableHeight)
            {
                if (!loading)
                {
                    if (isSearch)
                    {
                        LoadSearch();
                    }
                    else
                    {
                        if (isUser)
                        {
                           
                            LoadUser();
                        }
                        else
                        {
                            if (name == "隨機本子")
                            {
                                LoadRandom();
                            }
                            else
                            {
                                LoadData();
                            }
                        }

                    }


                    
                }
            }
        }

        private void ls_items_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is RandomModel)
            {
                MessageCenter.SendNavigateTo(NavigateMode.Info, typeof(InfoPage), new object[] { (e.ClickedItem as RandomModel)._id });
            }
            else
            {
                MessageCenter.SendNavigateTo(NavigateMode.Info, typeof(InfoPage), new object[] { (e.ClickedItem as ComicsModel)._id });
            }

        }


    }
    public class ComicsModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public ComicsModel data { get; set; }
        public ComicsModel comics { get; set; }
        public List<ComicsModel> docs { get; set; }
        public int total { get; set; }
        public int pages { get; set; }

        public string _id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int epsCount { get; set; }
        public int pagesCount { get; set; }
        public bool finished { get; set; }
        public List<string> categories { get; set; }

        public int likesCount { get; set; }

        public ComicsModel thumb { get; set; }
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
    public class RandomModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public RandomModel data { get; set; }
        public List<RandomModel> comics { get; set; }
        public int total { get; set; }
        public int pages { get; set; }

        public string _id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int epsCount { get; set; }
        public int pagesCount { get; set; }
        public bool finished { get; set; }
        public List<string> categories { get; set; }

        public int likesCount { get; set; }

        public RandomModel thumb { get; set; }
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

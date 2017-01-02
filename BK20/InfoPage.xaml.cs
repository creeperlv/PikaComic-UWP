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
using Windows.UI.Xaml.Media.Imaging;
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
            if (e.NavigationMode == NavigationMode.New)
            {
                pageN = 1;
                CpageN = 1;
                _id = (e.Parameter as object[])[0].ToString();
                LoadData();
                LoadComment();
            }
        }
        string _id = "";
        int pageN = 1;
        bool loading = false;
        private async void LoadData()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                btn_LoadMore.Visibility = Visibility.Collapsed;
                string uri = "";
               
                uri = "https://picaapi.picacomic.com/comics/" + _id;

                string results = await WebClientClass.GetResults(new Uri(uri));
                InfoModel info = JsonConvert.DeserializeObject<InfoModel>(results);
                if (info.code == 200)
                {
                    this.DataContext = info.data.comic;
                    if (info.data.comic.isFavourite)
                    {
                        btn_UnCollect.Visibility = Visibility.Visible;
                        btn_Collect.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btn_UnCollect.Visibility = Visibility.Collapsed;
                        btn_Collect.Visibility = Visibility.Visible;
                    }
                    if (info.data.comic.isLiked)
                    {
                        btn_UnLike.Visibility = Visibility.Visible;
                        btn_Like.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btn_UnLike.Visibility = Visibility.Collapsed;
                        btn_Like.Visibility = Visibility.Visible;
                    }
                    btn_LoadMore.Visibility = Visibility.Visible;
                    pr_Load.Visibility = Visibility.Collapsed;
                    LoadEp();
                }
                else
                {
                    messShow.Show(info.message, 2000);
                    pr_Load.Visibility = Visibility.Collapsed;
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
                pr_Load.Visibility = Visibility.Collapsed;
            }
             
        }
        private async void LoadEp()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                loading = true;
                if (pageN==1)
                {
                    list_E.Items.Clear();
                }
                string uri = "";

                uri = "https://picaapi.picacomic.com/comics/" + _id+ "/eps?page="+ pageN;

                string results = await WebClientClass.GetResults(new Uri(uri));
                EpsModel info = JsonConvert.DeserializeObject<EpsModel>(results);
              
                if (info.code == 200)
                {
                    if (info.data.eps.docs.Count != 0)
                    {
                        info.data.eps.docs.ForEach(x => list_E.Items.Add(x));
                        pageN++;
                    }
                    else
                    {
                        messShow.Show("沒有更多了", 2000);
                        btn_LoadMore.Visibility = Visibility.Collapsed;
                    }
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
                    messShow.Show("檢查你的網絡連接！", 3000);
                }
                else
                {
                    messShow.Show("讀取信息失敗了，挂個VPN試試？", 3000);
                }

            }
            finally
            {
                loading = false;
                pr_Load.Visibility = Visibility.Collapsed;

            }
        }

        int CpageN = 1;
        bool Cloading = false;
        private async void LoadComment()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                Cloading = true;
                if (CpageN == 1)
                {
                    ls_comment.Items.Clear();
                }
                string uri = "";

                uri = "https://picaapi.picacomic.com/comics/" + _id + "/comments?page=" + CpageN;

                string results = await WebClientClass.GetResults(new Uri(uri));
                CommentsModel info = JsonConvert.DeserializeObject<CommentsModel>(results);

                if (info.code == 200)
                {
                    if (info.data.comments.docs.Count != 0)
                    {
                        info.data.comments.docs.ForEach(x => ls_comment.Items.Add(x));
                        CpageN++;
                    }
                    else
                    {
                        messShow.Show("沒有更多了", 2000);
                        btn_LoadMore.Visibility = Visibility.Collapsed;
                    }
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
                    messShow.Show("檢查你的網絡連接！", 3000);
                }
                else
                {
                    messShow.Show("讀取信息失敗了，挂個VPN試試？", 3000);
                }

            }
            finally
            {
                Cloading = false;
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

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var x = new ContentDialog();
            StackPanel st = new StackPanel();
            st.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri((this.DataContext as InfoModel).thumb.image))
            });
            
            x.Content = st;
            x.PrimaryButtonText = "关闭";
            x.IsPrimaryButtonEnabled = true;
            await x.ShowAsync();
        }

        private void sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (sv.VerticalOffset == sv.ScrollableHeight)
            {
                if (!loading)
                {
                    LoadEp(); 
                }
            }
        }

        private void list_E_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<EpsModel> ls = new List<EpsModel>();
            foreach (EpsModel item in list_E.Items)
            {
                ls.Add(item);
            }
            MessageCenter.SendNavigateTo(NavigateMode.Play,typeof(ViewPage),new object[] { _id,e.ClickedItem,ls});
        }

        private void btn_LoadMore_Click(object sender, RoutedEventArgs e)
        {
            if (!loading)
            {
                LoadEp();
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ColumnPage), new object[] { (this.DataContext as InfoModel)._creator._id+","+(this.DataContext as InfoModel)._creator.name, false ,true});
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ColumnPage), new object[] { (this.DataContext as InfoModel).author, true, false });
        }

        private void ls_comment_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void sv_Comment_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (sv_Comment.VerticalOffset == sv_Comment.ScrollableHeight)
            {
                if (!Cloading)
                {
                    LoadComment();
                }
            }
        }

        private void btn_down_Click(object sender, RoutedEventArgs e)
        {
            messShow.Show("暫時還不支持離線~", 3000);
        }

        private void btn_Like_Click(object sender, RoutedEventArgs e)
        {
            Like(true);
        }

        private void btn_UnLike_Click(object sender, RoutedEventArgs e)
        {
            Like(false);
        }

        private void btn_Collect_Click(object sender, RoutedEventArgs e)
        {
            Collect(true);
        }

        private void btn_UnCollect_Click(object sender, RoutedEventArgs e)
        {
            Collect(false);
        }

        private async void Like(bool islike)
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
               
                string uri = "";

                uri = "https://picaapi.picacomic.com/comics/" + _id + "//like" ;

                string results = await WebClientClass.PostResults(new Uri(uri),"");
                CommentsModel info = JsonConvert.DeserializeObject<CommentsModel>(results);

                if (info.code == 200)
                {
                    if (islike)
                    {
                        btn_UnLike.Visibility = Visibility.Visible;
                        btn_Like.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btn_UnLike.Visibility = Visibility.Collapsed;
                        btn_Like.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    messShow.Show(info.message, 2000);

                }
                messShow.Show("操作成功辣！", 3000);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147012867)
                {
                    messShow.Show("檢查你的網絡連接！", 3000);
                }
                else
                {
                    messShow.Show("操作失敗了，挂個VPN試試？", 3000);
                }

            }
            finally
            {
                pr_Load.Visibility = Visibility.Collapsed;

            }
        }
        private async void Collect(bool iscollect)
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;

                string uri = "";

                uri = "https://picaapi.picacomic.com/comics/" + _id + "/favourite";

                string results = await WebClientClass.PostResults(new Uri(uri), "");
                CommentsModel info = JsonConvert.DeserializeObject<CommentsModel>(results);

                if (info.code == 200)
                {
                    if (iscollect)
                    {
                        btn_UnCollect.Visibility = Visibility.Visible;
                        btn_Collect.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btn_UnCollect.Visibility = Visibility.Collapsed;
                        btn_Collect.Visibility = Visibility.Visible;
                    }
                    messShow.Show("操作成功辣！", 3000);
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
                    messShow.Show("檢查你的網絡連接！", 3000);
                }
                else
                {
                    messShow.Show("操作失敗了，挂個VPN試試？", 3000);
                }

            }
            finally
            {
                pr_Load.Visibility = Visibility.Collapsed;

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
    public class EpsModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public EpsModel data { get; set; }
        public EpsModel eps { get; set; }
        public List<EpsModel> docs { get; set; }
        public int total { get; set; }
        public int pages { get; set; }

        public string _id { get; set; }
        public string title { get; set; }
        public string order { get; set; }
        public DateTime updated_at { get; set; }
     

    }

    public class CommentsModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public CommentsModel data { get; set; }
        public CommentsModel comments { get; set; }
        public List<CommentsModel> docs { get; set; }
        public int total { get; set; }
        public int pages { get; set; }

        public string _id { get; set; }
        public string content { get; set; }
        public CommentsModel _user { get; set; }
        public string gender { get; set; }
        public string name { get; set; }
        public bool verified { get; set; }
        public string exp { get; set; }
        public string level { get; set; }

        public CommentsModel avatar { get; set; }
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



        public string _comic { get; set; }
        public bool hide { get; set; }
        public DateTime created_at { get; set; }
        public int likesCount { get; set; }
        public int commentsCount { get; set; }
        public bool isLiked { get; set; }

        public string time { get {
                return created_at.ToString("yyyy-MM-dd");
            } }

    }

}

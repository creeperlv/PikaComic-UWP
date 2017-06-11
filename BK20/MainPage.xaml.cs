using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace BK20
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }
        bool IsClicks = false;
        private async void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (play_frame.CanGoBack)
            {
                e.Handled = true;
                play_frame.GoBack();
            }
            else
            {
                if (frame.CanGoBack)
                {
                    e.Handled = true;
                    frame.GoBack();
                }
                else
                {

                    if (e.Handled == false)
                    {
                        if (IsClicks)
                        {
                            Application.Current.Exit();
                        }
                        else
                        {
                            IsClicks = true;
                            e.Handled = true;
                            messShow.Show("再按一次退出应用", 1500);
                            await Task.Delay(1500);
                            IsClicks = false;
                        }
                    }

                }
            }
        }
        private void main_Home_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (main_Home.SelectedIndex)
            {
                case 0:
                    btn_Home.IsChecked = true;
                    btn_Cat.IsChecked = false;
                    btn_Member.IsChecked = false;
                    btn_Ssetting.IsChecked = false;
                    txt_Header.Text = "首頁";
                    if (ls_items.Items.Count == 0)
                    {
                        GetHome();
                    }


                    break;
                case 1:
                    btn_Home.IsChecked = false;
                    btn_Cat.IsChecked = true;
                    btn_Member.IsChecked = false;
                    btn_Ssetting.IsChecked = false;
                    txt_Header.Text = "分類";
                    if (list_Hot.Items.Count == 0)
                    {
                        GeHotWord();
                    }
                    if (gv_Cat.Items.Count == 0)
                    {
                        GetInfo();
                    }


                    break;
                case 2:
                    btn_Home.IsChecked = false;
                    btn_Cat.IsChecked = false;
                    btn_Member.IsChecked = true;
                    btn_Ssetting.IsChecked = false;
                    txt_Header.Text = "個人中心";
                    if (grid_profile.DataContext==null)
                    {
                        GetProFile();
                    }
                    break;
                case 3:
                    btn_Home.IsChecked = false;
                    btn_Cat.IsChecked = false;
                    btn_Member.IsChecked = false;
                    btn_Ssetting.IsChecked = true;
                    txt_Header.Text = "設定";
                    break;
                default:
                    break;
            }
        }

        private void btn_Cat_Checked(object sender, RoutedEventArgs e)
        {
            if (main_Home.SelectedIndex != 1)
            {
                main_Home.SelectedIndex = 1;
            }
        }

        private void btn_Home_Checked(object sender, RoutedEventArgs e)
        {
            if (main_Home.SelectedIndex != 0)
            {
                main_Home.SelectedIndex = 0;
            }
        }

        private void btn_Member_Checked(object sender, RoutedEventArgs e)
        {
            if (main_Home.SelectedIndex != 2)
            {
                main_Home.SelectedIndex = 2;
            }
        }

        private void btn_Ssetting_Checked(object sender, RoutedEventArgs e)
        {
            if (main_Home.SelectedIndex != 3)
            {
                main_Home.SelectedIndex = 3;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            txt_password.Password = SettingHelper.Get_StartPassword();
            if (txt_password.Password.Length!=0)
            {
                p_password.Visibility = Visibility.Visible;
            }
            frame.Visibility = Visibility.Visible;
            frame.Navigate(typeof(BlankPage));

            play_frame.Visibility = Visibility.Visible;
            play_frame.Navigate(typeof(BlankPage));
            MessageCenter.MianNavigateToEvent += MessageCenter_MianNavigateToEvent; ;
            MessageCenter.InfoNavigateToEvent += MessageCenter_InfoNavigateToEvent; ;
            MessageCenter.PlayNavigateToEvent += MessageCenter_PlayNavigateToEvent; ;



        }




        private void MessageCenter_PlayNavigateToEvent(Type page, params object[] par)
        {
            play_frame.Navigate(page, par);
        }

        private void MessageCenter_InfoNavigateToEvent(Type page, params object[] par)
        {
            frame.Navigate(page, par);
        }

        private void MessageCenter_MianNavigateToEvent(Type page, params object[] par)
        {
            this.Frame.Navigate(page, par);

        }
        bool loading = false;
        int homePage = 1;
        private async void GetHome()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                loading = true;
                string results = await WebClientClass.GetResults(new Uri("https://picaapi.picacomic.com/announcements?page=" + homePage));
                HomeModel lists = JsonConvert.DeserializeObject<HomeModel>(results);
                if (lists.code == 200)
                {
                    if (lists.data.announcements.docs.Count != 0)
                    {
                        lists.data.announcements.docs.ForEach(x => ls_items.Items.Add(x));
                        homePage++;
                    }
                    else
                    {
                        messShow.Show("沒有更多了", 2000);
                        btn_laodMore.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    messShow.Show(lists.message, 2000);

                }
            }
            catch (Exception ex)
            {
                if (ex.HResult== -2145844847)
                {
                    messShow.Show("登录失效，重新登录！", 3000);
                    SettingHelper.Set_Authorization("");
                    this.Frame.Navigate(typeof(LoginPage));
                    return;
                }
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

        private async void GetInfo()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                string results = await WebClientClass.GetResults(new Uri("https://picaapi.picacomic.com/categories"));
                CategoriesModel list = JsonConvert.DeserializeObject<CategoriesModel>(results);
                if (list.code == 200)
                {
                    List<CategoriesModel> ls = new List<CategoriesModel>();
                    ls.Add(new CategoriesModel() { title = "支持嗶咔", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_support.jpg" } });
                    ls.Add(new CategoriesModel() { title = "Avnight", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_love_pica.jpg" } });
                    ls.Add(new CategoriesModel() { title = "嗶咔排行榜", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_leaderboard.jpg" } });
                    ls.Add(new CategoriesModel() { title = "隨機本子", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_random.jpg" } });
                    ls.Add(new CategoriesModel() { title = "最近更新", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_latest.jpg" } });
                    list.data.categories.InsertRange(0, ls);
                    gv_Cat.ItemsSource = list.data.categories;
                }
                else
                {
                    messShow.Show(list.message, 3000);
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
        private async void GeHotWord()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                string results = await WebClientClass.GetResults(new Uri("https://picaapi.picacomic.com/keywords"));
                KeywordModel list = JsonConvert.DeserializeObject<KeywordModel>(results);
                list_Hot.ItemsSource = list.data.ls;
            }
            catch (Exception)
            {
                messShow.Show("讀取信息失敗了，挂個VPN試試？", 3000);
            }
            finally
            {
                pr_Load.Visibility = Visibility.Collapsed;
            }
        }

        private async void GetProFile()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                string results = await WebClientClass.GetResults(new Uri("https://picaapi.picacomic.com/users/profile"));
                ProfileModel list = JsonConvert.DeserializeObject<ProfileModel>(results);
                if (list.code == 200)
                {
                    grid_profile.DataContext = list.data.user;
                    if (list.data.user.isPunched)
                    {
                        btn_Punch.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btn_Punch.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    messShow.Show(list.message, 3000);
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


        private void main_Home_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int i = Convert.ToInt32(main_Home.ActualWidth / 100);

            ViewBox_num.Width = main_Home.ActualWidth / i - 11;
        }

        private void play_frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (play_frame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {

                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;


            }
        }

        private void frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (frame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {

                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            }

        }

        private void gv_Cat_ItemClick(object sender, ItemClickEventArgs e)
        {
            var info = e.ClickedItem as CategoriesModel;
            if (info.title== "嗶咔排行榜")
            {
                frame.Navigate(typeof(RankPage));
                return;
            }
            if (info.title == "Avnight")
            {
                frame.Navigate(typeof(AVNightPage));
                return;
            }
            if (info.title == "支持嗶咔")
            {

                return;
            }
            frame.Navigate(typeof(ColumnPage),new object[]{ info.title, false, false });
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            messShow.Show("嗶咔~嗶咔~", 3000);
        }

        private async void ls_items_ItemClick(object sender, ItemClickEventArgs e)
        {
            var info = e.ClickedItem as HomeModel;
            var x = new ContentDialog();
            ScrollViewer s = new ScrollViewer() {
                VerticalScrollBarVisibility= ScrollBarVisibility.Auto
            };

            StackPanel st = new StackPanel();
            st.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(info.thumb.image))
            });
            st.Children.Add(new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                IsTextSelectionEnabled = true,
                Text = info.title,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            st.Children.Add(new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                IsTextSelectionEnabled = true,
                FontSize=14,
                Foreground=new SolidColorBrush(Colors.Gray),
                Text = info.created_at.ToString(),
                HorizontalAlignment= HorizontalAlignment.Center
            });
            st.Children.Add(new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                IsTextSelectionEnabled = true,
                Text = info.content
            });
            s.Content = st;
            x.Content = s;
            x.PrimaryButtonText = "知道了";
            x.IsPrimaryButtonEnabled = true;
            await x.ShowAsync();
        }

        private void sv_home_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (sv_home.VerticalOffset == sv_home.ScrollableHeight)
            {
                if (!loading)
                {
                    GetHome();
                }
            }
        }

        private void btn_laodMore_Click(object sender, RoutedEventArgs e)
        {
            if (!loading)
            {
                GetHome();
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            frame.Navigate(typeof(ColumnPage), new object[] { txt_Search.Text, true ,false});
        }

        private void list_Hot_ItemClick(object sender, ItemClickEventArgs e)
        {
            frame.Navigate(typeof(ColumnPage), new object[] { (e.ClickedItem as KeywordModel).keyword, true,false });
        }

        private async void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            switch ((sender as HyperlinkButton).Tag.ToString())
            {
                case "faq":
                    frame.Navigate(typeof(WebPage));
                    break;
                case "about":
                    frame.Navigate(typeof(AboutPage));
                    break;
                case "sm":
                    string info = @"01、本程序无任何盈利行为，开放源码于GITHUB上，仅供学习交流编程技术使用
02、本程序为哔咔漫画第三方UWP，资源均来自哔咔(http://picacomic.com/)
03、禁止将本程序任意传播,传播软件可能造成的任何法律和刑事事件本人不负任何责任
04、任何个人使用引起的法律和刑事事件本人将不负任何责任";
                    await new MessageDialog(info).ShowAsync();
                    break;            

                case "logout":
                    SettingHelper.Set_Authorization("");
                    this.Frame.Navigate(typeof(LoginPage));
                    break;
                case "exit":
                    Application.Current.Exit();
                    break;
                default:
                    break;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SettingHelper.Set_StartPassword(txt_password.Password);
        }


        private void start_pwd_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (start_pwd.Password==txt_password.Password)
                {
                    p_password.Visibility = Visibility.Collapsed;
                }
                else
                {
                    messShow.Show("密码错误",3000);
                }
            }
        }

        private void HyperlinkButton_Click_2(object sender, RoutedEventArgs e)
        {
            switch ((sender as HyperlinkButton).Tag.ToString())
            {
                case "colloct":
                    frame.Navigate(typeof(FavouritePage));
                    break;
                default:
                    messShow.Show("還沒完工...", 3000);
                    break;
            }
          
        }

        private async void btn_Punch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                string results = await WebClientClass.PostResults(new Uri("https://picaapi.picacomic.com/users/punch-in"),"");
                PunchModel list = JsonConvert.DeserializeObject<PunchModel>(results);
                if (list.code == 200)
                {
                   
                    btn_Punch.Visibility = Visibility.Collapsed;
                    messShow.Show("操作成功辣！", 3000);
                    GetProFile();
                }
                else
                {
                    messShow.Show(list.message, 3000);
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

    public class CategoriesModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public CategoriesModel data { get; set; }
        public List<CategoriesModel> categories { get; set; }

        public string _id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public CategoriesModel thumb { get; set; }
        public string originalName { get; set; }
        public string path { get; set; }
        public string fileServer { get; set; }
        private string _img;
        public string image
        {
            get
            {
                if (fileServer != null && fileServer.Length != 0)
                {
                    return fileServer + "/static/" + path;
                }
                else
                {
                    return _img;
                }
            }
            set { _img = value; }
        }

    }

    public class HomeModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public HomeModel data { get; set; }
        public HomeModel announcements { get; set; }
        public List<HomeModel> docs { get; set; }

        public string _id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }


        public HomeModel thumb { get; set; }
        public string originalName { get; set; }
        public string path { get; set; }
        public string fileServer { get; set; }
        private string _img;
        public string image
        {
            get
            {
                if (fileServer != null && fileServer.Length != 0)
                {
                    return fileServer + "/static/" + path;
                }
                else
                {
                    return _img;
                }
            }
            set { _img = value; }
        }

    }
    public class KeywordModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public KeywordModel data { get; set; }
        public List<string> keywords { get; set; }
        public string keyword { get; set; }
        public List<KeywordModel> ls
        {
            get
            {
                List<KeywordModel> ls = new List<KeywordModel>();
                keywords.ForEach(x => ls.Add(new KeywordModel() { keyword = x }));

                return ls;
            }
        }


    }

    public class ProfileModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public ProfileModel data { get; set; }

        public ProfileModel user { get; set; }

        public string _id { get; set; }
        public string birthday { get; set; }
        public string email { get; set; }
        public string keyword { get; set; }
        public string gender { get; set; }
        public string name { get; set; }
        public string activation_date { get; set; }
        public bool verified { get; set; }
        public int exp { get; set; }
        public int level { get; set; }
        public string created_at { get; set; }
        public bool isPunched { get; set; }
    }

    public class PunchModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public PunchModel data { get; set; }

        public PunchModel res { get; set; }

        public string punchInLastDay{ get; set; }
        public string status { get; set; }
        
    }
}

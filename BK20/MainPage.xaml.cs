using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
                    break;
                case 1:
                    btn_Home.IsChecked = false;
                    btn_Cat.IsChecked = true;
                    btn_Member.IsChecked = false;
                    btn_Ssetting.IsChecked = false;
                    txt_Header.Text = "分類";
                    break;
                case 2:
                    btn_Home.IsChecked = false;
                    btn_Cat.IsChecked = false;
                    btn_Member.IsChecked = true;
                    btn_Ssetting.IsChecked = false;
                    txt_Header.Text = "個人中心";
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
            if (main_Home.SelectedIndex!=1)
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
            frame.Visibility = Visibility.Visible;
            frame.Navigate(typeof(BlankPage));

            play_frame.Visibility = Visibility.Visible;
            play_frame.Navigate(typeof(BlankPage));
            MessageCenter.MianNavigateToEvent += MessageCenter_MianNavigateToEvent; ;
            MessageCenter.InfoNavigateToEvent += MessageCenter_InfoNavigateToEvent; ;
            MessageCenter.PlayNavigateToEvent += MessageCenter_PlayNavigateToEvent; ;
           
           
            GetInfo();
            GeHotWord();
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


        private async void GetInfo()
        {
            try
            {
                //pr_Load.Visibility = Visibility.Visible;
                string results = await WebClientClass.GetResults(new Uri("https://picaapi.picacomic.com/categories"));
                CategoriesModel list = JsonConvert.DeserializeObject<CategoriesModel>(results);
                if (list.code == 200)
                {
                    List<CategoriesModel> ls = new List<CategoriesModel>();
                    ls.Add(new CategoriesModel() { title = "支持哔咔", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_support.jpg" } });
                    ls.Add(new CategoriesModel() { title = "哔咔聊天室", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_love_pica.jpg" } });
                    ls.Add(new CategoriesModel() { title = "哔咔排行榜", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_leaderboard.jpg" } });
                    ls.Add(new CategoriesModel() { title = "隨機本子", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_random.jpg" } });
                    ls.Add(new CategoriesModel() { title = "最近更新", thumb = new CategoriesModel() { image = "ms-appx:///Assets/Cat/cat_latest.jpg" } });
                    list.data.categories.InsertRange(0, ls);
                    gv_Cat.ItemsSource = list.data.categories;
                }
                else
                {
                    messShow.Show(list.message,3000);
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
                //pr_Load.Visibility = Visibility.Collapsed;
            }
        }
        private async void GeHotWord()
        {
            try
            {
                //pr_Load.Visibility = Visibility.Visible;
                string results = await WebClientClass.GetResults(new Uri("https://picaapi.picacomic.com/keywords"));
                KeywordModel list = JsonConvert.DeserializeObject<KeywordModel>(results);
                list_Hot.ItemsSource = list.data.ls;
            }
            catch (Exception)
            {
                 messShow.Show("加载失败，翻墙后试试？", 3000);
            }
            finally
            {
                //pr_Load.Visibility = Visibility.Collapsed;
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
            frame.Navigate(typeof(ColumnPage), info.title);
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
                if (fileServer!=null&&fileServer.Length!=0)
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
                keywords.ForEach(x => ls.Add(new KeywordModel() { keyword =x }));
               
                return ls;
            }
        }
       

    }
}

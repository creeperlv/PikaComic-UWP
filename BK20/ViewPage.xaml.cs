using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class ViewPage : Page
    {
        public ViewPage()
        {
            this.InitializeComponent();
        }
        private void Image_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ScrollViewer sv = ((Image)sender).Parent as ScrollViewer;
            if (sv.HorizontalScrollBarVisibility == ScrollBarVisibility.Disabled)
            {
                sv.ZoomMode = ZoomMode.Enabled;
                sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                sv.HorizontalScrollMode = ScrollMode.Auto;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                sv.VerticalScrollMode = ScrollMode.Auto;
                ((Image)sender).Stretch = Stretch.None;
                //txt_zoom.Text ="缩放模式";
            }
            else
            {
                sv.ZoomToFactor(1);
                sv.ZoomMode = ZoomMode.Disabled;
                sv.HorizontalScrollMode = ScrollMode.Disabled;
                sv.VerticalScrollMode = ScrollMode.Disabled;
                ((Image)sender).Stretch = Stretch.Uniform;
                sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                //txt_zoom.Text = "正常模式";
            }

        }

        private void grid_Center_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //await new MessageDialog("Center").ShowAsync();
            if (grid_Content.Visibility == Visibility.Visible)
            {
                grid_Content.Visibility = Visibility.Collapsed;
            }
            else
            {
                grid_Content.Visibility = Visibility.Visible;
            }
        }

        private void Image_Holding(object sender, HoldingRoutedEventArgs e)
        {
            MenuFlyout.ShowAttachedFlyout((Image)sender);
        }


        private void Image_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            MenuFlyout.ShowAttachedFlyout((Image)sender);
        }
        int epPage = 0;
        string Id = "";
       
        List<EpsModel> ls = new List<EpsModel>();
        DispatcherTimer datetimer = new DispatcherTimer();//用于更新时间
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            datetimer.Interval = new TimeSpan(0, 0, 1);
            datetimer.Tick += Datetimer_Tick;
            datetimer.Start();
            Id = ((object[])e.Parameter)[0].ToString();
             
            ls = (List<EpsModel>)((object[])e.Parameter)[2];

            list_E.ItemsSource = ls;
            list_E.SelectedIndex = ls.IndexOf((EpsModel)((object[])e.Parameter)[1]);

        }

        private async void Datetimer_Tick(object sender, object e)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                txt_timer.Text = DateTime.Now.ToLocalTime().ToString("HH:mm");
                //switch (CheckNetworkHelper.CheckInternetConnectionType())
                //{
                //    case InternetConnectionType.WwanConnection:
                //        top_txt_NetType.Text = "移动数据";
                //        top_txt_NetType.Foreground = new SolidColorBrush(Colors.OrangeRed);
                //        break;
                //    case InternetConnectionType.WlanConnection:
                //        top_txt_NetType.Text = "WIFI";
                //        top_txt_NetType.Foreground = new SolidColorBrush(Colors.White);
                //        break;
                //    default:
                //        break;
                //}

            });
        }

        WebClientClass wc = new WebClientClass();
        private async void GetList()
        {
            try
            {
                txt_Loading.Visibility = Visibility.Collapsed;
                pr.Visibility = Visibility.Visible;
                wc = new WebClientClass();
                txt_Count.Text = string.Empty;
                txt_zoom.Text = (list_E.SelectedItem as EpsModel).title;
                List<EpModel> ls1 = new List<EpModel>();
                while (true)
                {
                    string results = await WebClientClass.GetResults(new Uri(string.Format("https://picaapi.picacomic.com/comics/{0}/order/{1}/pages?page={2}", Id,(list_E.SelectedItem as EpsModel).order, epPage)));
                    JObject obj = JObject.Parse(results);
                   
                    if ((int)obj["code"] == 200)
                    {
                        List<EpModel> epList = JsonConvert.DeserializeObject<List<EpModel>>(obj["data"]["pages"]["docs"].ToString());
                        if (epList.Count != 0)
                        {
                            epList.ForEach(x => ls1.Add(x));
                            epPage++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                fv.ItemsSource = ls1;
                txt_Count.Text = ls1.Count.ToString();
                txt_Loading.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                messShow.Show("加载失败，请刷新", 2000);
            }
            finally
            {
                pr.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void list_E_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void list_E_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ls.Count != 0)
            {
                btn_SelectE.Flyout.Hide();
                fv.ItemsSource = null;
                epPage = 1;
               // epPage = 0;
                GetList();
            }
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if (list_E.SelectedIndex + 1 >= list_E.Items.Count)
            {
                messShow.Show("后面没有了", 2000);
            }
            else
            {
                list_E.SelectedIndex = list_E.SelectedIndex + 1;
            }

        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            fv.ItemsSource = null;
            GetList();
        }

        private void btn_Forward_Click(object sender, RoutedEventArgs e)
        {
            if (epPage - 2 < 0)
            {
                messShow.Show("前面没有章节", 2000);
            }
            else
            {
                list_E.SelectedIndex = epPage - 2;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txt_page.Text,out int _int))
            {
                if (_int==0|| _int==fv.Items.Count)
                {
                    return;
                }
                fv.SelectedIndex = _int;
            }
        }
    }
    public class EModel
    {
        public int index { get; set; }

    }
    public class EpModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public EpModel data { get; set; }
        public EpModel pages { get; set; }
        public List<EpModel> docs { get; set; }

        public EpModel media { get; set; }
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

        public string _id { get; set; }


    }


    public class DataConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((int)value + 1).ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class DataConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((double)value).ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

}

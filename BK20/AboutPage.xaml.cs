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
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
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
            txt_log.Text = log;
            txt_ver.Text = SettingHelper.GetVersion();
        }

        string log = @"
Ver 2.0.2.0(2017-1-10)

1、修复无法登录

Ver 2.0.1.0(2017-1-1)

1、修复无法注册
2、修复评论重复

Ver 2.0.0.0(2017-1-1)

首个2.0版本上线，相对1.X版更新
1、UI大改，更加美观易用
2、支持登录后同步收藏历史
3、API升级，更加稳定
4、更多细节性增强

匆忙赶着上线，很多都没功能都没完成，见谅";


    }
}

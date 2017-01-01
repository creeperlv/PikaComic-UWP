using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            l_txt_email.Text = SettingHelper.Get_Email();
        }
        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (l_txt_email.Text.Length==0||l_txt_password.Password.Length==0)
            {
                messShow.Show("(#`O′)，檢查下你的輸入！", 3000);
                return;
            }
            Login(l_txt_email.Text, l_txt_password.Password);


        }

        private async void Login(string email,string password)
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                LoginM lm = new LoginM()
                {
                    email=email,
                    password=password
                };
                string results = await WebClientClass.PostResults_Login(new Uri("https://picaapi.picacomic.com/auth/sign-in"),JsonConvert.SerializeObject(lm));
                LoginModel re = JsonConvert.DeserializeObject<LoginModel>(results);
                if (re.code==200)
                {
                    SettingHelper.Set_Authorization(re.data.token);
                    SettingHelper.Set_Email(email);
                    SettingHelper.Set_Password(password);
                    this.Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    messShow.Show(re.message,3000);
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
                    messShow.Show("登錄失敗了,檢查下你的輸入？", 3000);
                }
            }
            finally
            {
                pr_Load.Visibility = Visibility.Collapsed;
            }
        }
        private async void Sign(string email, string password,string name,string date,string gender)
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                SignM lm = new SignM()
                {
                    email = email,
                    password = password,
                    birthday=date,
                    name=name,
                    gender=gender
                };
                string results = await WebClientClass.PostResults_Login(new Uri("https://picaapi.picacomic.com/auth/register"), JsonConvert.SerializeObject(lm));
                LoginModel re = JsonConvert.DeserializeObject<LoginModel>(results);
                if (re.code == 200)
                {
                    SettingHelper.Set_Email(email);
                    SettingHelper.Set_Password(password);
                    p_Login.Visibility = Visibility.Visible;
                    p_Sign.Visibility = Visibility.Collapsed;
                    l_txt_email.Text = SettingHelper.Get_Email();
                    l_txt_password.Password = SettingHelper.Get_Password();
                    Login(l_txt_email.Text, l_txt_password.Password);
                }
                else
                {
                    messShow.Show(re.message, 3000);
                    pr_Load.Visibility = Visibility.Collapsed;
                }

            }
            catch (Exception ex)
            {
                pr_Load.Visibility = Visibility.Collapsed;
                if (ex.HResult == -2147012867)
                {
                    messShow.Show("檢查你的網絡連接！", 3000);
                }
                else
                {
                    messShow.Show("登錄失敗了，挂個VPN試試？", 3000);
                }
            }
           
        }

        private void btn_Sign_Click(object sender, RoutedEventArgs e)
        {
            if (s_txt_email.Text.Length == 0 || s_txt_password.Password.Length == 0 || s_txt_password_a.Password.Length == 0||s_txt_name.Text.Length==0||s_date.Date==null)
            {
                messShow.Show("(#`O′)，檢查下你的輸入！", 3000);
                return;
            }
            if (!Regex.IsMatch(s_txt_email.Text,@".*?@.*?\..*?"))
            {
                messShow.Show("郵箱輸入格式錯誤", 3000);
                return;
            }
            if ((DateTime.Now.Year - s_date.Date.Year) < 18)
            {
                messShow.Show("未滿18的小朋友不適合用這應用哦！", 3000);
                return;
            }

            if (s_txt_password.Password.Length<8)
            {
                messShow.Show("密碼太短了，至少要8位", 3000);
                return;
            }

            if (s_txt_password.Password!=s_txt_password_a.Password)
            {
                messShow.Show("兩次密碼不一致", 3000);
                return;
            }

           

            Sign(s_txt_email.Text, s_txt_password.Password, s_txt_name.Text, s_date.Date.Date.ToString("yyyy-MM-dd"), "bot");
        }

        private void btn_GoSign_Click(object sender, RoutedEventArgs e)
        {
            p_Login.Visibility = Visibility.Collapsed;
            p_Sign.Visibility = Visibility.Visible;
            p_Find_Password.Visibility = Visibility.Collapsed;
            p_Resend_activation.Visibility = Visibility.Collapsed;
        }

        private void btn_GoLogin_Click(object sender, RoutedEventArgs e)
        {
            p_Login.Visibility = Visibility.Visible;
            p_Sign.Visibility = Visibility.Collapsed;
            p_Find_Password.Visibility = Visibility.Collapsed;
            p_Resend_activation.Visibility = Visibility.Collapsed;
        }

        private void btn_SendEmail_Click(object sender, RoutedEventArgs e)
        {
            if (r_txt_email.Text.Length==0)
            {
                messShow.Show("(#`O′)，檢查下你的輸入！", 3000);
                return;
            }
            SendEmail(r_txt_email.Text);
        }

        private async void SendEmail(string email)
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                SignM lm = new SignM()
                {
                    email = email,
                   
                };
                string results = await WebClientClass.PostResults_Login(new Uri("https://picaapi.picacomic.com/auth/resend-activation"), JsonConvert.SerializeObject(lm));
                LoginModel re = JsonConvert.DeserializeObject<LoginModel>(results);
                if (re.code == 200)
                {
                    messShow.Show("已發送", 3000);
                }
                else
                {
                    messShow.Show(re.message, 3000);
                    pr_Load.Visibility = Visibility.Collapsed;
                }

            }
            catch (Exception ex)
            {
                pr_Load.Visibility = Visibility.Collapsed;
                if (ex.HResult == -2147012867)
                {
                    messShow.Show("檢查你的網絡連接！", 3000);
                }
                else
                {
                    messShow.Show("發送失敗了，挂個VPN試試？", 3000);
                }
            }
        }
        private async void FindPassword(string email)
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                SignM lm = new SignM()
                {
                    email = email,

                };
                string results = await WebClientClass.PostResults_Login(new Uri(" https://picaapi.picacomic.com/auth/forgot-password"), JsonConvert.SerializeObject(lm));
                LoginModel re = JsonConvert.DeserializeObject<LoginModel>(results);
                if (re.code == 200)
                {
                    messShow.Show("已發送", 3000);
                }
                else
                {
                    messShow.Show(re.message, 3000);
                    pr_Load.Visibility = Visibility.Collapsed;
                }

            }
            catch (Exception ex)
            {
                pr_Load.Visibility = Visibility.Collapsed;
                if (ex.HResult == -2147012867)
                {
                    messShow.Show("檢查你的網絡連接！", 3000);
                }
                else
                {
                    messShow.Show("發送失敗了，挂個VPN試試？", 3000);
                }
            }
        }
        private void btn_SendEmail_f_Click(object sender, RoutedEventArgs e)
        {
            if (f_txt_email.Text.Length == 0)
            {
                messShow.Show("(#`O′)，檢查下你的輸入！", 3000);
                return;
            }
            FindPassword(f_txt_email.Text);
        }

        private void btn_findp_Click(object sender, RoutedEventArgs e)
        {
            p_Login.Visibility = Visibility.Collapsed;
            p_Sign.Visibility = Visibility.Collapsed;
            p_Find_Password.Visibility = Visibility.Visible;
            p_Resend_activation.Visibility = Visibility.Collapsed;
        }

        private void btn_sende_Click(object sender, RoutedEventArgs e)
        {
            p_Login.Visibility = Visibility.Collapsed;
            p_Sign.Visibility = Visibility.Collapsed;
            p_Find_Password.Visibility = Visibility.Collapsed;
            p_Resend_activation.Visibility = Visibility.Visible;
        }
    }
    public class EmailM
    {
        public string email { get; set; }
    }
    public class LoginM
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class SignM
    {
        public string email { get; set; }
        public string password { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public string name { get; set; }
    }
    public class LoginModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public LoginModel data { get; set; }
        public string token { get; set; }
    }
}

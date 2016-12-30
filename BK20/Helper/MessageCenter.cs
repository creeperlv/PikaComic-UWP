using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK20
{
    public enum NavigateMode
    {
        Main,
        Info,
        Home,
        Play
    }
    public delegate void MessageHandel(object par, params object[] par1);
    public delegate void NavigateHandel(Type page, params object[] par);
    public delegate void LoginedHandel();
    public static class MessageCenter
    {
        public static event MessageHandel ChanageThemeEvent;
        public static event LoginedHandel Logined;
        public static void SendLogined()
        {
            Logined();
        }
        public static void SendChanageThemeEvent(object par, params object[] par1)
        {
            ChanageThemeEvent(par, par1);
        }
        public static event NavigateHandel InfoNavigateToEvent;
        public static event NavigateHandel PlayNavigateToEvent;
        public static event NavigateHandel MianNavigateToEvent;
        public static event NavigateHandel HomeNavigateToEvent;
        public static void SendNavigateTo(NavigateMode mode, Type page, params object[] par)
        {
            switch (mode)
            {
                case NavigateMode.Main:
                    MianNavigateToEvent(page, par);
                    break;
                case NavigateMode.Info:
                    InfoNavigateToEvent(page, par);
                    break;
                case NavigateMode.Play:
                    PlayNavigateToEvent(page, par);
                    break;
                case NavigateMode.Home:
                    HomeNavigateToEvent(page, par);
                    break;
                default:
                    break;
            }

        }

    }
}

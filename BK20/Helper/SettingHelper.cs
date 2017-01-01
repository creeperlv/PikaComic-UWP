using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace BK20
{
    public static class SettingHelper
    {
        static ApplicationDataContainer container;
        static PackageId pack = (Package.Current).Id;
        public static string GetVersion()
        {
            return string.Format("{0}.{1}.{2}.{3}", pack.Version.Major, pack.Version.Minor, pack.Version.Build, pack.Version.Revision);
        }

        public static bool IsLogned()
        {
            if (Get_Authorization().Length!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public static string Get_Email()
        {
            container = ApplicationData.Current.LocalSettings;
            if (container.Values["Email"] != null)
            {
                return container.Values["Email"].ToString();
            }
            else
            {
                container.Values["Email"] = "";
                return "";
            }
        }

        public static void Set_Email(string value)
        {
            container = ApplicationData.Current.LocalSettings;
            container.Values["Email"] = value;
        }

        public static string Get_Password()
        {
            container = ApplicationData.Current.LocalSettings;
            if (container.Values["Password"] != null)
            {
                return container.Values["Password"].ToString();
            }
            else
            {
                container.Values["Password"] = "";
                return "";
            }
        }

        public static void Set_Password(string value)
        {
            container = ApplicationData.Current.LocalSettings;
            container.Values["Password"] = value;
        }



        public static string Get_Authorization()
        {
            container = ApplicationData.Current.LocalSettings;
            if (container.Values["Authorization"] != null)
            {
                return container.Values["Authorization"].ToString();
            }
            else
            {
                container.Values["Authorization"] = "";
                return "";
            }
        }

        public static void Set_Authorization(string value)
        {
            container = ApplicationData.Current.LocalSettings;
            container.Values["Authorization"] = value;
        }

        public static string Get_StartPassword()
        {
            container = ApplicationData.Current.LocalSettings;
            if (container.Values["StartPassword"] != null)
            {
                return container.Values["StartPassword"].ToString();
            }
            else
            {
                container.Values["StartPassword"] = "";
                return "";
            }
        }

        public static void Set_StartPassword(string value)
        {
            container = ApplicationData.Current.LocalSettings;
            container.Values["StartPassword"] = value;
        }




    }
}

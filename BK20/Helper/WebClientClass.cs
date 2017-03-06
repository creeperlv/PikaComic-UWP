using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;


namespace BK20
{
    public class WebClientClass
    {
        public WebClientClass()
        {
            //Encoding.RegisterProvider(provider);
          
        }
        public static async Task<string> GetResults(Uri url)
        {
                using (HttpClient hc = new HttpClient())
                {
                /*
                 api-key: C69BAF41DA5ABD1FFEDC6D2FEA56B
accept: application/vnd.picacomic.com.v1+json
app-version: 2.0.3.13
app-uuid: c9b31154-fd5a-35ff-8a27-5f4a3f3bb278
app-platform: android
app-build-version: 29
Host: picaapi.picacomic.com
Connection: Keep-Alive
Accept-Encoding: gzip
User-Agent: okhttp/3.2.0

                 */
                hc.DefaultRequestHeaders.Add("authorization", SettingHelper.Get_Authorization());
                hc.DefaultRequestHeaders.Add("api-key", "C69BAF41DA5ABD1FFEDC6D2FEA56B");
                hc.DefaultRequestHeaders.Add("accept", "application/vnd.picacomic.com.v1+json");
                hc.DefaultRequestHeaders.Add("app-version", " 2.0.3.13");
                //hc.DefaultRequestHeaders.Add("app-uuid", "adf860ea-c460-328a-b126-5fa01b634ed8");
                hc.DefaultRequestHeaders.Add("app-uuid", Guid.NewGuid().ToString());
                hc.DefaultRequestHeaders.Add("app-platform", "android");
                hc.DefaultRequestHeaders.Add("User-Agent", "okhttp/3.2.0");
                HttpResponseMessage hr = await hc.GetAsync(url);
                    hr.EnsureSuccessStatusCode();
                    string results = await hr.Content.ReadAsStringAsync();
                    return results;
                }
        }

        public static async Task<IBuffer> GetBuffer(Uri url)
        {
            using (HttpClient hc = new HttpClient())
            {
                HttpResponseMessage hr = await hc.GetAsync(url);
                hr.EnsureSuccessStatusCode();
                IBuffer results = await hr.Content.ReadAsBufferAsync();
                return results;
            }
        }
        public static async Task<string> PostResults_Login(Uri url, string PostContent)
        {

                using (HttpClient hc = new HttpClient())
                {
                    //hc.DefaultRequestHeaders.Add("authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InhpYW95YW9jekA1MnV3cC5jb20iLCJyb2xlIjoibWVtYmVyIiwibmFtZSI6InhpYW95YW9jeiIsInZlcnNpb24iOiIyLjAuMy41IiwiaWF0IjoxNDgyOTEzMDAxLCJleHAiOjE0ODM1MTc4MDF9.p3WvelMcEAB9DnguuBIn9gBv7Um6swscZ7-aM5dzXeo");
                    hc.DefaultRequestHeaders.Add("api-key", "C69BAF41DA5ABD1FFEDC6D2FEA56B");
                    hc.DefaultRequestHeaders.Add("accept", "application/vnd.picacomic.com.v1+json");
                    hc.DefaultRequestHeaders.Add("app-version", " 2.0.3.13");
                hc.DefaultRequestHeaders.Add("app-uuid", Guid.NewGuid().ToString());
                //hc.DefaultRequestHeaders.Add("app-uuid", "adf860ea-c460-328a-b126-5fa01b634ed8");
                hc.DefaultRequestHeaders.Add("app-platform", "android");
                    hc.DefaultRequestHeaders.Add("User-Agent", "okhttp/3.2.0");
                    var response = await hc.PostAsync(url, new HttpStringContent(PostContent, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }

        }
        public static async Task<string> PostResults(Uri url, string PostContent)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("authorization", SettingHelper.Get_Authorization());
                    hc.DefaultRequestHeaders.Add("api-key", "C69BAF41DA5ABD1FFEDC6D2FEA56B");
                    hc.DefaultRequestHeaders.Add("accept", "application/vnd.picacomic.com.v1+json");
                 
                    hc.DefaultRequestHeaders.Add("app-version", " 2.0.3.13");
                    hc.DefaultRequestHeaders.Add("app-uuid", Guid.NewGuid().ToString());
                    //hc.DefaultRequestHeaders.Add("app-uuid", "adf860ea-c460-328a-b126-5fa01b634ed8");
                    hc.DefaultRequestHeaders.Add("app-platform", "android");
                    hc.DefaultRequestHeaders.Add("User-Agent", "okhttp/3.2.0");
                    var response = await hc.PostAsync(url, new HttpStringContent(PostContent, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static async Task<string> GetResultsUTF8Encode(Uri url)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    HttpResponseMessage hr = await hc.GetAsync(url);
                    hr.EnsureSuccessStatusCode();
                    var encodeResults = await hr.Content.ReadAsBufferAsync();
                    string results = Encoding.UTF8.GetString(encodeResults.ToArray(), 0, encodeResults.ToArray().Length);
                    return results;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static async Task<string> GetResultsUTF8Encode_Phone(Uri url)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 5_0 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9A334 Safari/7534.48.3");
                    //hc.DefaultRequestHeaders.Add("Referer", url.AbsolutePath);
                    HttpResponseMessage hr = await hc.GetAsync(url);
                    hr.EnsureSuccessStatusCode();
                    var encodeResults = await hr.Content.ReadAsBufferAsync();
                    string results = Encoding.UTF8.GetString(encodeResults.ToArray(), 0, encodeResults.ToArray().Length);
                    return results;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static async Task<string> GetResultsGBKEncode(Uri url)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    HttpResponseMessage hr = await hc.GetAsync(url);
                    hr.EnsureSuccessStatusCode();
                    var encodeResults = await hr.Content.ReadAsBufferAsync();
                    byte[] by = encodeResults.ToArray();
                 
                    by = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.UTF8, by);
                    string results = Encoding.GetEncoding("gb2312").GetString(encodeResults.ToArray(), 0, encodeResults.ToArray().Length);
                    return results;
                    
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
       
    }

 
}

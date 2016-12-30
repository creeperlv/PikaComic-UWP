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
    class WebClientClass
    {
        public WebClientClass()
        {
            //Encoding.RegisterProvider(provider);
          
        }
        public async Task<string> GetResults(Uri url)
        {
                using (HttpClient hc = new HttpClient())
                {
                hc.DefaultRequestHeaders.Add("authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InhpYW95YW9jekA1MnV3cC5jb20iLCJyb2xlIjoibWVtYmVyIiwibmFtZSI6InhpYW95YW9jeiIsInZlcnNpb24iOiIyLjAuMy41IiwiaWF0IjoxNDgyOTEzMDAxLCJleHAiOjE0ODM1MTc4MDF9.p3WvelMcEAB9DnguuBIn9gBv7Um6swscZ7-aM5dzXeo");
                hc.DefaultRequestHeaders.Add("api-key", "C69BAF41DA5ABD1FFEDC6D2FEA56B");
                hc.DefaultRequestHeaders.Add("accept", "application/vnd.picacomic.com.v1+json");
                hc.DefaultRequestHeaders.Add("app-version", " 2.0.3.5");
                hc.DefaultRequestHeaders.Add("app-uuid", "adf860ea-c460-328a-b126-5fa01b634ed8");
                hc.DefaultRequestHeaders.Add("app-platform", "android");
                hc.DefaultRequestHeaders.Add("User-Agent", "okhttp/3.2.0");
                HttpResponseMessage hr = await hc.GetAsync(url);
                    hr.EnsureSuccessStatusCode();
                    string results = await hr.Content.ReadAsStringAsync();
                    return results;
                }
        }

        public async Task<IBuffer> GetBuffer(Uri url)
        {
            using (HttpClient hc = new HttpClient())
            {
                HttpResponseMessage hr = await hc.GetAsync(url);
                hr.EnsureSuccessStatusCode();
                IBuffer results = await hr.Content.ReadAsBufferAsync();
                return results;
            }
        }

        public async Task<string> PostResults(Uri url, string PostContent)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Referer = new Uri("http://www.bilibili.com/");
                    var response = await hc.PostAsync(url, new HttpStringContent(PostContent, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/x-www-form-urlencoded"));
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

        public async Task<string> GetResultsUTF8Encode(Uri url)
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
        public async Task<string> GetResultsUTF8Encode_Phone(Uri url)
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

        public async Task<string> GetResultsGBKEncode(Uri url)
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

    public class DHModel
    {
        public object result { get; set; }
        public object list { get; set; }

        public object recommends { get; set; }
        public string aid { get; set; }
        public string title { get; set; }
        public string play { get; set; }
        public string video_review { get; set; }
        public string mid { get; set; }
        public string pic { get; set; }
        public string author { get; set; }

        public object banners { get; set; }
        public string img { get; set; }

        public object news { get; set; }

    }
}

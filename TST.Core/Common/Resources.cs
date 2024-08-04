using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;

namespace Common
{
    public class Resources
    {
        private readonly string rootPath;
        private readonly IHttpContextAccessor httpContextAccessor;

        public Resources(IHttpContextAccessor httpContextAccessor, IHostEnvironment env)
        {
            this.httpContextAccessor = httpContextAccessor;
            rootPath = Path.Combine(env.ContentRootPath, "wwwroot", "resources");
        }

        public JObject Load()
        {
            string path = Path.Combine(rootPath, Lang + ".json");
            JObject o = null;
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                o = JObject.Parse(json);
            }
            return o;
        }

        public string Mapping(string en, string th) => Lang == "th" ? th : en;

        public string Lang => httpContextAccessor.HttpContext.Request.Cookies["Lang"] == null ? "en" : httpContextAccessor.HttpContext.Request.Cookies["Lang"].ToString();

        public string BuildWord(string keys)
        {
            var x = keys.Split(",").Select(s => s.Trim()).Where(w => w != "").Distinct();

            return string.Join(", ", x);
        }

        public string ToPhone66(string value)
        {
            if (value?.StartsWith("0") == true)
            {
                string x = value.Replace("-", "").Replace(" ", "");
                return "+66" + x.Substring(1, x.Length - 1);
            }

            return value;
        }
    }
}

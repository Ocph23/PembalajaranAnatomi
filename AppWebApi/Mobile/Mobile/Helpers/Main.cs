using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Helpers
{
    public static class Main
    {
        private static string _server = "http://192.168.1.7/";

        public static async Task<MainPage> GetMainPageAsync()
        {
            var x = await Task.FromResult(Xamarin.Forms.Application.Current.MainPage);
            return x as MainPage;
        }

        public static AuthenticationToken Token { get; set; }
        public static string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }
    }
}

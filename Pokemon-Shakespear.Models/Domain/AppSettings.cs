using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon_Shakespear.Models.Domain
{
    public class AppSettings
    {
        public string[] AllowedOrigins { get; set; }
        public string ShakespeareApi { get; set; }
        public int AbsoluteExpirationInHrs { get; set; }
        public int SlidingExpirationInMinutes { get; set; }

        public AppSettings()
        {

        }
    }
}

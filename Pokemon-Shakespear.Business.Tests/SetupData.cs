using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pokemon_Shakespear.Business.Tests
{
    public static class SetupData
    {
        public static T GetData<T>(string fileName)
        {
            string text = File.ReadAllText(fileName);
            var data = JsonConvert.DeserializeObject<T>(text);
            return data;
        }
    }
}

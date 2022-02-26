using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Interfaces
{ 
    public interface IShakespeareApiService
    {
        Task<string> GetTranslation(string textToTranslate);
    }
}

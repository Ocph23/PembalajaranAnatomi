using Mobile.Helpers;
using Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(Mobile.Services.SoalDataStore))]
namespace Mobile.Services
{
    public class SoalDataStore : IDataStore<soal>
    {
        public Task<bool> AddItemAsync(soal item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<soal> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<soal>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<soal>> GetItemsAsync(int Id)
        {
            var data = new List<soal>();
            using (var service = new RestClient())
            {
                try
                {
                    var response = await service.GetAsync(string.Format("api/{0}/soal",Id));
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<soal>>(content);
                        foreach (var item in Items)
                        {
                            data.Add(item);
                        }
                        return await Task.FromResult(data);
                    }
                    else
                    {
                        throw new System.Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {

                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Error",
                        Message = ex.Message,
                        Cancel = "OK"
                    }, "message");
                    return await Task.FromResult(data);
                }
            }
        }

        public Task<bool> UpdateItemAsync(soal item)
        {
            throw new NotImplementedException();
        }
    }
}

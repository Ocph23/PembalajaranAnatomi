using Mobile.Helpers;
using Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(Mobile.Services.MateriDataStore))]
namespace Mobile.Services
{
    public class MateriDataStore : IDataStore<materi>
    {
        bool isInitialized;
        List<materi> items;
        public Task<bool> AddItemAsync(materi item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<materi> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<materi>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
                isInitialized = false;
            await InitializeAsync();
            return await Task.FromResult(items);

        }

        public Task<IEnumerable<materi>> GetItemsAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<materi>();
            using (var service = new RestClient())
            {
                try
                {
                    var response = await service.GetAsync("api/materi");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<materi>>(content);
                        foreach (var item in Items)
                        {
                            items.Add(item);
                        }

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
                }
                finally
                {
                    isInitialized = true;
                }
            }


        }

        public Task<bool> UpdateItemAsync(materi item)
        {
            throw new NotImplementedException();
        }
    }
}

using Mobile.Helpers;
using Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(Mobile.Services.SubMateriDataStore))]
namespace Mobile.Services
{
    public class SubMateriDataStore : IDataStore<submateri>
    {
        bool isInitialized;
        List<submateri> items;
        public Task<bool> AddItemAsync(submateri item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<submateri> GetItemAsync(string id)
        {
            var Id = Convert.ToInt32(id);
            using (var service = new RestClient())
            {
                try
                {
                    var url = string.Format("/api/submateri?id={0}", Id);
                    var response = await service.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Item = JsonConvert.DeserializeObject<submateri>(content);
                        return await Task.FromResult(Item);
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

                    submateri result = null;
                    return await Task.FromResult(result);
                }
               
            }
        }

        public async Task<IEnumerable<submateri>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(submateri item)
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAsync( int Id)
        {
            items = new List<submateri>();
            using (var service = new RestClient())
            {
                try
                {
                    var url = string.Format("api/{0}/submateri", Id);
                    var response = await service.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<submateri>>(content);
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

        public async Task<IEnumerable<submateri>> GetItemsAsync(int Id)
        {
            await InitializeAsync(Id);
            return await Task.FromResult(items);

        }
    }
}

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
    public class SoalDataStore : IDataStore<kuis>
    {
        public Task<bool> AddItemAsync(kuis item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<kuis> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<kuis>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<kuis>> GetItemsAsync(string Id)
        {
            var data = new List<kuis>();
            using (var service = new RestClient())
            {
                try
                {
                    var response = await service.GetAsync(string.Format("api/{0}/soal",Id));
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<kuis>>(content);
                        foreach (var item in Items)
                        {
                            item.Choices = new List<Option>();
                            item.Choices.Add(GenerateOptionTrueAnswer(item.JawabanA,item.JawabanBenar));
                            item.Choices.Add(GenerateOptionTrueAnswer(item.JawabanB, item.JawabanBenar));
                            item.Choices.Add(GenerateOptionTrueAnswer(item.JawabanC, item.JawabanBenar));
                            item.Choices.Add(GenerateOptionTrueAnswer(item.JawabanD, item.JawabanBenar));
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

        private Option GenerateOptionTrueAnswer(string item, string value)
        {
            var opt = new Option { Value = item };
            if (item == value)
                opt.IsTrueAnswer = true;

            return opt;
          
        }

        public Task<IEnumerable<kuis>> GetItemsAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(kuis item)
        {
            throw new NotImplementedException();
        }
    }
}

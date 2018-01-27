using Mobile.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using Mobile.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace Mobile.ViewModels
{
    internal class KuisViewModel:BaseViewModel
    {
        public string Nama { get; }

        private submateri subitem;
        private INavigation navigation;
        private kuis _soal;
        public ObservableCollection<kuis> Soals { get; set; }
        public Command LoadItemsCommand { get; }
        public Command NextCommand { get; }

        public kuis Soal {
            get { return _soal; }
            set
            {
                SetProperty(ref _soal, value);
            }
        }

        public int QuizPosition { get; private set; }

        public KuisViewModel(submateri subitem, INavigation navigation,string nama)
        {
            this.Nama = nama;
            this.subitem = subitem;
            this.navigation = navigation;
            Soals = new ObservableCollection<Models.kuis>();
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
            NextCommand = new Command((x) => NextCommandAction(x));
            ExecuteLoadItemsCommand(null);
        }

        private async void NextCommandAction(object x)
        {
            if (Soal.OptionSelected == null)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Pilih Jawaban Anda",
                    Cancel = "OK"
                }, "message");
            }
            else
            {
                QuizPosition++;
                if (QuizPosition<=Soals.Count())
                {
                   Soal = Soals.Where(O => O.Number == QuizPosition).FirstOrDefault();
                }else
                {
                    var benar = Soals.Where(O => O.OptionSelected.IsTrueAnswer).Count();
                    await navigation.PushAsync(new Views.QuizFinishView(Soals.Count(),benar,Nama));
                }
                
            }
        }

        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Soals.Clear();
                var datas = await SoalDataStore.GetItemsAsync(subitem.KodeSubMateri);
                if(datas.Count()>0)
                {
                    var ramdomData = ShuffleList<kuis>(datas.ToList());
                    var i = 1;
                    foreach (var item in ramdomData)
                    {
                        var aData = item;
                     //   aData.Choices = ShuffleList<Option>(item.Choices);
                        item.Number = i;
                        Soals.Add(aData);
                        i++;
                    }

                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Info",
                        Message = "Baca soal dengan baik sebelum menjawab !",
                        Cancel = "OK"
                    }, "message");
                    Soal = Soals[0];
                    QuizPosition++;
                }
                else
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Info",
                        Message = "Daftar Belum Tersedia",
                        Cancel = "OK"
                    }, "message");
                    await navigation.PopAsync();
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
                IsBusy = false;
            }
        }

     

      

        public static List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); 
                randomList.Add(inputList[randomIndex]); 

                inputList.RemoveAt(randomIndex);
            }

            return randomList; //return the new random list
        }
    }


    public enum QuizStatus
    {
        None,Start,End
    }
}
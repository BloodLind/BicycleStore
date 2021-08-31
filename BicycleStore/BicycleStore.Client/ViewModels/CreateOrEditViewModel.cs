using BicycleStore.Client.Infrastructure;
using BicycleStore.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BicycleStore.Client.ViewModels
{
    class CreateOrEditViewModel:BaseNotifyPropertyChanged
    {

        private Bicycle bicycle { get; set; }
        public Bicycle Bicycle { get {
                return bicycle;
                    } set { bicycle = value;Notify(); } }
        private HttpClient Client { get; set; }
        public CreateOrEditViewModel()
        {
            Bicycle = new Bicycle();
            Client = new HttpClient();
            InitializeCommands();
        }
        public ICommand CreateOrEdit { set; get; }
        public ICommand Cancle { set; get; }
        public void InitializeCommands()
        {
            CreateOrEdit = new RelayCommand(async x =>
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync($"{NetworkManager.DomenName}/api/Bicycles", Bicycle);
                response.EnsureSuccessStatusCode();
            });

            Cancle = new RelayCommand(x =>
            {
            });
        }
        public void SetObjectForEding(Bicycle bicycle)
        {
            Bicycle = bicycle;
        }
    }
}

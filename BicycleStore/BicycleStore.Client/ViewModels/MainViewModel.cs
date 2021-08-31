using BicycleStore.Client.Infrastructure;
using BicycleStore.Client.Models;
using BicycleStore.Client.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BicycleStore.Client.ViewModels
{
    class MainViewModel
    {


        public Bicycle Selected { set; get; }

        public ObservableCollection<Bicycle> Bicycles { get; set; }

        private  HttpClient Client {get;set;}

        public ICommand Add { set; get; }
        public ICommand Remove { set; get; }
        public ICommand Edit { set; get; }
        public MainViewModel()
        {
            Client = new HttpClient();
            Bicycles = new ObservableCollection<Bicycle>();
          
            InitializeCommands();
            RefreshList();
        }

        private async void RefreshList()
        {
            HttpResponseMessage response = await Client.GetAsync($"{NetworkManager.DomenName}/api/Bicycles");
            if (response.IsSuccessStatusCode)
            {
                var bicycles = await response.Content.ReadAsAsync<List<Bicycle>>();
                Bicycles.Clear();
                foreach (var bicycle in bicycles)
                    Bicycles.Add(bicycle);
            }
        }

        public void InitializeCommands()
        {
            Add = new RelayCommand(x =>
            {



                CreateOrEditView view = new Views.CreateOrEditView() { 
                    Owner = Application.Current.MainWindow ,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner };
                view.ShowDialog();
                RefreshList();

            });
            Remove = new RelayCommand(async x =>
            {

                HttpResponseMessage response = await Client.DeleteAsync($"{NetworkManager.DomenName}/api/Bicycles/{Selected.Id}");

                response.EnsureSuccessStatusCode();
                RefreshList();
            });
            Edit = new RelayCommand(x =>
            {
                CreateOrEditView view = new Views.CreateOrEditView()
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

               CreateOrEditViewModel viewModel =  view.DataContext as CreateOrEditViewModel;
                viewModel.SetObjectForEding(Selected);
                
                view.ShowDialog();
                RefreshList();
            });
        }
        
    }
}

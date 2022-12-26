using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OpenWeatherAPI.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<DataSource> _dataSources;

        #region [Properties]

        #region [Title]

        private string? _title = "Main window";

        public string? Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        #endregion [Title]

        #region [Data Sources]

        public ObservableCollection<DataSource> DataSources { get; set; } = new ObservableCollection<DataSource>();

        #endregion [Data Sources]

        #endregion [Properties]

        #region [Commands]

        private ICommand _loadDataSourcesCommand;

        public ICommand LoadDataSourcesCommand => _loadDataSourcesCommand
            ??= new LambdaCommand(OnLoadDataSources);

        private async void OnLoadDataSources(object? p)
        {
            DataSources.Clear();
            foreach (var item in await _dataSources.GetAll())
                DataSources.Add(item);
        }

        #endregion [Commands]

        public MainWindowViewModel(IRepository<DataSource> dataSources)
        {
            this._dataSources = dataSources;
        }
    }
}
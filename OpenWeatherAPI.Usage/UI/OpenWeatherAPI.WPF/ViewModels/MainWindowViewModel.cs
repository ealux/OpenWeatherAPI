using MathCore.WPF.ViewModels;

namespace OpenWeatherAPI.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region [Properties]

        #region [Title]

        private string? _title = "Main window";

        public string? Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        #endregion [Title]

        #endregion [Properties]
    }
}
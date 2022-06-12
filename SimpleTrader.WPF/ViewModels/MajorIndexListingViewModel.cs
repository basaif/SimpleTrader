using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        private MajorIndex _dowJones = new();
        private MajorIndex _nasdaq = new();
        private MajorIndex _sP500 = new();

        public MajorIndex DowJones
        {
            get
            {
                return _dowJones;
            }

            set
            {
                _dowJones = value;
                OnPropertyChanged(nameof(DowJones));
            }
        }
        public MajorIndex Nasdaq
        {
            get
            {
                return _nasdaq;
            }

            set
            {
                _nasdaq = value;
                OnPropertyChanged(nameof(Nasdaq));
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public MajorIndex SP500
        {
            get
            {
                return _sP500;
            }

            set
            {
                _sP500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }
        public ICommand LoadMajorIndexesCommand { get; }
        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            LoadMajorIndexesCommand = new LoadMajorIndexesCommand(this, majorIndexService);
        }

        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new(majorIndexService);

            majorIndexViewModel.LoadMajorIndexesCommand.Execute(null);

            return majorIndexViewModel;
        }
    }
}

using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        private readonly IMajorIndexService _majorIndexService;
        private MajorIndex _dowJones = new();
        private MajorIndex _nasdaq = new();
        private MajorIndex _sP500 = new();

        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public MajorIndex DowJones
        {
            get
            {
                //_dowJones.Type = MajorIndexType.DowJones;
                //_dowJones.Price = 765.54;
                //_dowJones.Changes = 32.32;
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
                //_nasdaq.Type = MajorIndexType.Nasdaq;
                //_nasdaq.Price = 4324243.54;
                //_nasdaq.Changes = -332432.32;
                return _nasdaq;
            }

            set
            {
                _nasdaq = value;
                OnPropertyChanged(nameof(Nasdaq));
            }
        }
        public MajorIndex SP500
        {
            get
            {
                //_sP500.Type = MajorIndexType.SP500;
                //_sP500.Price = 3244.22;
                //_sP500.Changes = 9009.21;
                return _sP500;
            }

            set
            {
                _sP500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }
        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new(majorIndexService);
            majorIndexViewModel.LoadMajorIndexes();
            return majorIndexViewModel;
        }
        private void LoadMajorIndexes()
        {
            _majorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith(task =>
            {
                if (task.Exception is null)
                {
                    DowJones = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq).ContinueWith(task =>
            {
                if (task.Exception is null)
                {
                    Nasdaq = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith(task =>
            {
                if (task.Exception is null)
                {
                    SP500 = task.Result;
                }
            });
        }
    }
}

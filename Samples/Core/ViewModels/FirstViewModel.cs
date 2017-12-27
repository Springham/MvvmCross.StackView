using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.StackView.Sample.Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public FirstViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public MvxCommand GoToAutoPage => new MvxCommand(() => _navigationService.Navigate<AutoViewModel>());

        public MvxCommand GoToManualPage => new MvxCommand(() => _navigationService.Navigate<ManualViewModel>());
    }
}
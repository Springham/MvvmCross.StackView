using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using MvvmCross.StackView.Sample.Core.ViewModels;

namespace MvvmCross.StackView.Sample.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterNavigationServiceAppStart<FirstViewModel>();
        }
    }
}

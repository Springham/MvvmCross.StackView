using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.StackView.Sample.Core.ViewModels;

namespace MvvmCross.StackView.Sample.iOS.Views
{
    [MvxFromStoryboard]
    public partial class FirstView : MvxViewController
    {
        public FirstView(IntPtr handle) 
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(AutoButton).To(vm => vm.GoToAutoPage);
            set.Bind(ManualButton).To(vm => vm.GoToManualPage);
            set.Apply();
        }
    }
}
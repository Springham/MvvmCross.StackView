using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.StackView.Sample.Core.ViewModels;

namespace MvvmCross.StackView.Sample.iOS.Views
{
    public class AutoView : MvxViewController
    {
        public AutoView(IntPtr handle) 
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var stackView = new MvxStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            View.AddSubview(stackView);

            stackView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            stackView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
            stackView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            stackView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;

            var set = this.CreateBindingSet<AutoView, AutoViewModel>();
            set.Bind(stackView).For(view => view.ItemsSource).To(vm => vm.ListItems);
            set.Apply();
        }
    }
}
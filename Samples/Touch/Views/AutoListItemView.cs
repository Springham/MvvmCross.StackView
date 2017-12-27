using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.StackView.Sample.Core.ViewModels;
using UIKit;

namespace MvvmCross.StackView.Sample.iOS.Views
{
    public class AutoListItemView : MvxViewController
    {
        public AutoListItemView(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var titleLabel = new UILabel
            {
                Font = UIFont.SystemFontOfSize(17),
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            View.AddSubview(titleLabel);

            titleLabel.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor).Active = true;
            titleLabel.LeftAnchor.ConstraintEqualTo(View.LeftAnchor, 16).Active = true;

            var subtitleLable = new UILabel
            {
                Font = UIFont.SystemFontOfSize(12),
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            View.AddSubview(subtitleLable);

            subtitleLable.LeftAnchor.ConstraintEqualTo(titleLabel.LeftAnchor).Active = true;
            subtitleLable.TopAnchor.ConstraintEqualTo(titleLabel.BottomAnchor, 6).Active = true;

            var set = this.CreateBindingSet<AutoListItemView, AutoListItem>();
            set.Bind(titleLabel).To(vm => vm.Title);
            set.Bind(subtitleLable).To(vm => vm.Subtitle);
            set.Apply();
        }
    }
}
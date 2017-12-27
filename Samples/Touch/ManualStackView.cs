using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.StackView.Sample.Core.ViewModels;
using UIKit;

namespace MvvmCross.StackView.Sample.iOS
{
    public class ManualStackView : MvxStackView
    {
        protected override UIView GetView(MvxViewModel viewModel, int index)
        {
            return new ManualListItemView
            {
                DataContext = viewModel
            };
        }

        private class ManualListItemView : MvxView
        {
            public ManualListItemView()
            {
                var titleLabel = new UILabel
                {
                    Font = UIFont.SystemFontOfSize(17),
                    TranslatesAutoresizingMaskIntoConstraints = false
                };

                AddSubview(titleLabel);

                titleLabel.CenterYAnchor.ConstraintEqualTo(CenterYAnchor).Active = true;
                titleLabel.LeftAnchor.ConstraintEqualTo(LeftAnchor, 16).Active = true;

                var subtitleLable = new UILabel
                {
                    Font = UIFont.SystemFontOfSize(12),
                    TranslatesAutoresizingMaskIntoConstraints = false
                };

                AddSubview(subtitleLable);

                subtitleLable.LeftAnchor.ConstraintEqualTo(titleLabel.LeftAnchor).Active = true;
                subtitleLable.TopAnchor.ConstraintEqualTo(titleLabel.BottomAnchor, 6).Active = true;

                this.DelayBind(() =>
                {
                    var set = this.CreateBindingSet<ManualListItemView, ManualListItem>();
                    set.Bind(titleLabel).To(vm => vm.Title);
                    set.Bind(subtitleLable).To(vm => vm.Subtitle);
                    set.Apply();
                });
            }
        }
    }
}
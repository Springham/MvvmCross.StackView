using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using MvvmCross.Binding.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using MvvmCross.WeakSubscription;
using UIKit;

namespace MvvmCross.StackView
{
    public class MvxStackView : UIStackView
    {
        private IEnumerable<MvxViewModel> _itemsSource;
        private MvxNotifyCollectionChangedEventSubscription _subscription;
        private IDictionary<MvxViewModel, UIView> _viewModelViewLinks;

        public MvxStackView()
        {
            Initialise();
        }

        public MvxStackView(IntPtr handler)
            : base(handler)
        {
            Initialise();
        }

        [MvxSetToNullAfterBinding]
        public virtual IEnumerable<MvxViewModel> ItemsSource
        {
            get => _itemsSource;
            set
            {
                if (ReferenceEquals(_itemsSource, value))
                {
                    return;
                }

                _itemsSource = value;

                if (_itemsSource is INotifyCollectionChanged collectionChanged)
                {
                    _subscription = collectionChanged.WeakSubscribe(OnCollectionChanged);
                }

                InitialiseContainer();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_subscription != null)
                {
                    _subscription.Dispose();
                    _subscription = null;
                }
            }

            base.Dispose(disposing);
        }

        protected virtual UIView GetView(MvxViewModel viewModel, int index)
        {
            if (!(Mvx.Resolve<IMvxIosViewCreator>().CreateView(viewModel) is UIViewController viewController))
            {
                return null;
            }

            return GetView(viewController, index);
        }

        protected virtual UIView GetView(UIViewController controller, int index)
        {
            return controller.View;
        }

        protected virtual void OnBeforeAdd(UIView view)
        {
        }

        protected virtual void OnAfterAdd(UIView view)
        {
        }

        protected virtual void OnBeforeRemove(UIView view)
        {
        }

        protected virtual void OnAfterRemove(UIView view)
        {
        }

        private void Initialise()
        {
            _viewModelViewLinks = new Dictionary<MvxViewModel, UIView>();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Reset)
            {
                var viewModels = _viewModelViewLinks.Select(viewModelLink => viewModelLink.Key).ToList();

                foreach (var viewModel in viewModels)
                {
                    RemoveViewModel(viewModel);
                }

                return;
            }
            
            if (notifyCollectionChangedEventArgs.NewItems != null)
            {
                int newStartingIndex = notifyCollectionChangedEventArgs.NewStartingIndex;

                foreach (var newItem in notifyCollectionChangedEventArgs.NewItems)
                {
                    AddViewModel(newItem as MvxViewModel, newStartingIndex);
                    newStartingIndex++;
                }
            }
            
            if (notifyCollectionChangedEventArgs.OldItems != null)
            {
                foreach (var oldItem in notifyCollectionChangedEventArgs.OldItems)
                {
                    RemoveViewModel(oldItem as MvxViewModel);
                }
            }
        }

        private void InitialiseContainer()
        {
            var index = 0;

            ClearSubviews();

            foreach (var viewModel in ItemsSource)
            {
                AddViewModel(viewModel, index);
                index++;
            }
        }

        private void AddViewModel(MvxViewModel viewModel, int index)
        {
            InvokeOnMainThread(() =>
            {
                if (index == -1)
                {
                    index = 0;
                }

                var view = GetView(viewModel, index);

                if (view == null)
                {
                    return;
                }

                _viewModelViewLinks.Add(viewModel, view);

                OnBeforeAdd(view);

                InsertArrangedSubview(view, (nuint)index);

                OnAfterAdd(view);
            });
        }

        private void RemoveViewModel(MvxViewModel viewModel)
        {
            InvokeOnMainThread(() =>
            {
                if (!_viewModelViewLinks.ContainsKey(viewModel))
                {
                    return;
                }

                var view = _viewModelViewLinks[viewModel];

                OnBeforeRemove(view);

                RemoveArrangedSubview(view);
                view.RemoveFromSuperview();
                _viewModelViewLinks.Remove(viewModel);

                OnAfterRemove(view);
            });
        }

        private void ClearSubviews()
        {
            foreach (var subview in ArrangedSubviews)
            {
                RemoveArrangedSubview(subview);
                subview.RemoveFromSuperview();
            }

            _viewModelViewLinks.Clear();
        }
    }
}
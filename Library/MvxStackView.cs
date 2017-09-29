using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MvvmCross.Binding.Attributes;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.WeakSubscription;
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
            get { return _itemsSource; }
            set
            {
                if (ReferenceEquals(_itemsSource, value))
                {
                    return;
                }

                _itemsSource = value;

                var collectionChanged = _itemsSource as INotifyCollectionChanged;

                if (collectionChanged != null)
                {
                    _subscription = collectionChanged.WeakSubscribe(OnCollectionChanged);
                }

                InitialiseContainer();
            }
        }

        private void Initialise()
        {
            _viewModelViewLinks = new Dictionary<MvxViewModel, UIView>();
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

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var newItems = notifyCollectionChangedEventArgs?.NewItems;
            if (newItems != null)
            {
                var newStartingIndex = notifyCollectionChangedEventArgs.NewStartingIndex;

                foreach (var newItem in notifyCollectionChangedEventArgs.NewItems)
                {
                    AddViewModel(newItem as MvxViewModel, newStartingIndex);
                    newStartingIndex++;
                }
            }

            var oldItems = notifyCollectionChangedEventArgs?.OldItems;
            if (oldItems != null)
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
            _viewModelViewLinks.Clear();
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

                var view = GetView(viewModel);

                if (view == null)
                {
                    return;
                }

                _viewModelViewLinks.Add(viewModel, view);

                OnBeforeAdd(view);

                InsertArrangedSubview(view, (nuint) index);

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

        protected virtual UIView GetView(MvxViewModel viewModel)
        {
            var viewController = Mvx.Resolve<IMvxIosViewCreator>().CreateView(viewModel) as UIViewController;

            if (viewController == null)
            {
                return null;
            }

            return GetView(viewController);
        }

        protected virtual UIView GetView(UIViewController controller)
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
    }
}

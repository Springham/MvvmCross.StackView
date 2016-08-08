## MvvmCross UIStackView for iOS

UIStackView that can have a list of ViewModels bound such that Views can be dynamically added and removed from the ViewModel layer.

### Setup
* Available on NuGet: http://www.nuget.org/packages/Springham.MvvmCross.StackView  [![NuGet](https://img.shields.io/nuget/v/Springham.MvvmCross.StackView.svg?label=NuGet)](https://www.nuget.org/packages/Springham.MvvmCross.StackView/)
* Install into your Xamarin.iOS project

### Usage

Bind to the source with a collection of MvxViewModels

`set.Bind(MvxStackView).For(stackView => stackView.ItemsSource).To(vm => vm.Sections);`

In order for this work each ViewModel must have an associated View through MvvmCross.

To provide a custom or wrapper view override GetView and return your view.

`protected virtual UIView GetView(UIViewController controller)`

Hooks are available to perform custom actions before/after adding and removing of views. These can be used to perform animations for example.

`protected virtual void OnBeforeAdd(UIView view){}`
`protected virtual void OnAfterAdd(UIView view){}`
`protected virtual void OnBeforeRemove(UIView view){}`
`protected virtual void OnAfterRemove(UIView view){}`

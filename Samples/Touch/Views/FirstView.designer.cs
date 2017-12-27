// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MvvmCross.StackView.Sample.iOS.Views
{
    [Register ("FirstView")]
    partial class FirstView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AutoButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ManualButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AutoButton != null) {
                AutoButton.Dispose ();
                AutoButton = null;
            }

            if (ManualButton != null) {
                ManualButton.Dispose ();
                ManualButton = null;
            }
        }
    }
}
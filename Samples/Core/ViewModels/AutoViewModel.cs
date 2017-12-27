using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.StackView.Sample.Core.ViewModels
{
    public class AutoViewModel : MvxViewModel
    {
        public MvxObservableCollection<AutoListItem> ListItems => new MvxObservableCollection<AutoListItem>();

        public override async Task Initialize()
        {
            for (var index = 0; index < 10; index++)
            {
                var listItem = new AutoListItem
                {
                    Title = $"Title {index}",
                    Subtitle = $"Subtitle {index}"
                };

                ListItems.Add(listItem);
            }
        }
    }
}
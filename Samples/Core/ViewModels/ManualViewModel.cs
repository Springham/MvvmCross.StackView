using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.StackView.Sample.Core.ViewModels
{
    public class ManualViewModel : MvxViewModel
    {
        public MvxObservableCollection<ManualListItem> ListItems => new MvxObservableCollection<ManualListItem>();

        public override async Task Initialize()
        {
            for (var index = 0; index < 5; index++)
            {
                var listItem = new ManualListItem
                {
                    Title = $"Title {index}",
                    Subtitle = $"Subtitle {index}"
                };

                ListItems.Add(listItem);
            }
        }
    }
}
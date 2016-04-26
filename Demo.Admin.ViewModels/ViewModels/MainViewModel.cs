using Core.Common.UI.Core;
using System.ComponentModel.Composition;

namespace Demo.Admin.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainViewModel : ViewModelBase
    {
        [Import]
        public MaintainProductsViewModel MaintainProductsViewModel { get; private set; }
    }
}

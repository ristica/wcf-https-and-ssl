using Core.Common.Core;
using Demo.Admin.ViewModels;
using MahApps.Metro.Controls;

namespace Demo.Admin.Client
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            main.DataContext = ObjectBase.Container.GetExportedValue<MainViewModel>();
        }
    }
}

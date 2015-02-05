using System.Windows;

namespace Lugs.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.GroupFeedBack = "--------------";
            mainWindowViewModel.UserFeedBack = "--------------";
            mainWindowViewModel.IsInGroupFeedback = "--------------";
            this.DataContext = mainWindowViewModel;
        }
    }
}

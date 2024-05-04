using System.Windows;
using Task3_1.Model;
using Task3_1.ViewModel;

namespace Task3_1.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModelClass vmc = new ViewModelClass(new ModelClass());
        DataContext = vmc;
    }
}
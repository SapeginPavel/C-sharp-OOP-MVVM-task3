using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task3_1.Model;
using Task3_1.ViewModel;

namespace Task3_1.View;

public partial class MainWindow : Window
{
    // private ObservableCollection<string> _constructorInfos;
    public MainWindow()
    {
        InitializeComponent();
        ViewModelClass vmc = new ViewModelClass(new ModelClass());
        DataContext = vmc;

        // ConstructorInfos = new ObservableCollection<string>();
        // ConstructorInfosView.ItemsSource = ConstructorInfos;

        // vmc.PropertyChanged += (sender, e) =>
        // {
        //     if (e.PropertyName == nameof(vmc.Constructors))
        //     {
        //         Console.WriteLine("ЗАШЛИ в мэйн виндов");
        //         ConstructorInfos.Clear();
        //         foreach (ConstructorInfo constructorInfo in vmc.Constructors)
        //         {
        //             Console.WriteLine("Оно работает!");
        //             ConstructorInfos.Add(constructorInfo.ToString());
        //             Console.WriteLine(constructorInfo.ToString());
        //             Console.WriteLine(ConstructorInfos[0]);
        //             Console.WriteLine("Готово");
        //         }
        //     }
        // };
    }

    // public ObservableCollection<string> ConstructorInfos
    // {
    //     get => _constructorInfos;
    //     set => _constructorInfos = value ?? throw new ArgumentNullException(nameof(value));
    // }
}
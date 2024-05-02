using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Task3_1.Model;

namespace Task3_1.ViewModel;

class ViewModelClass : INotifyPropertyChanged
{
    private ObservableCollection<Type> _classes;
    private Type? _selectedClass;

    private Dictionary<string, MethodInfo> mapStringMethodInfos;
    private ObservableCollection<string> _methods;
    private string _selectedMethod;
    
    private Dictionary<string, ConstructorInfo> mapStringConstructorInfos;
    private ObservableCollection<string> _constructors;
    private string _selectedConstr;

    private ModelClass _modelClass;

    public ViewModelClass(ModelClass modelClass)
    {
        _modelClass = modelClass;
        Classes = new ObservableCollection<Type>();
        Methods = new ObservableCollection<string>();
        Constructors = new ObservableCollection<string>();

        mapStringConstructorInfos = new Dictionary<string, ConstructorInfo>();
        mapStringMethodInfos = new Dictionary<string, MethodInfo>();
        
        modelClass.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(modelClass.Classes))
            {
                Classes.Clear();
                foreach (Type t in modelClass.Classes)
                {
                    Classes.Add(t);
                }
            } else if (e.PropertyName == nameof(modelClass.Constructors))
            {
                Constructors.Clear();
                mapStringConstructorInfos.Clear();
                foreach (var constructor in modelClass.Constructors)
                {
                    string name = createNameForMethod(constructor);
                    Constructors.Add(name);
                    mapStringConstructorInfos.Add(name, constructor);
                }
            }  else if (e.PropertyName == nameof(modelClass.Methods))
            {
                Methods.Clear();
                mapStringMethodInfos.Clear();
                foreach (var method in modelClass.Methods)
                {
                    string name = createNameForMethod(method);
                    Methods.Add(name);
                    mapStringMethodInfos.Add(name, method);
                }
            }
        };
    }

    public ObservableCollection<Type> Classes
    {
        get => _classes;
        set
        {
            _classes = value;
        }
    }

    public ObservableCollection<string> Constructors
    {
        get => _constructors;
        set => _constructors = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ObservableCollection<string> Methods
    {
        get => _methods;
        set => _methods = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Dictionary<string, ConstructorInfo> MapStringConstructorInfos
    {
        get => mapStringConstructorInfos;
        set => mapStringConstructorInfos = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public Type? SelectedClass
    {
        get => _selectedClass;
        set
        {
            _modelClass.SelectedClass = value;
            _selectedClass = value;
        }
    }

    public string SelectedConstr
    {
        get => _selectedConstr;
        set
        {
            _selectedConstr = value;
            _modelClass.SelectedConstructor = mapStringConstructorInfos[value];
        }
    }

    public string SelectedMethod
    {
        get => _selectedMethod;
        set
        {
            _selectedMethod = value;
            _modelClass.SelectedMethod = mapStringMethodInfos[value];
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    private CommandClass openFileCommand;
    public CommandClass OpenFileCommand
    {
        get
        {
            if (openFileCommand == null)
            {
                openFileCommand = new CommandClass(o =>
                {
                    OpenFileDialogAction(o);
                });
            }
            return openFileCommand;
        }
    }
    
    private void OpenFileDialogAction(object parameter)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Только dll (*.dll)|*.dll";
        if (openFileDialog.ShowDialog() == true)
        {
            string selectedFilePath = openFileDialog.FileName;
            _modelClass.PathToAssembly = selectedFilePath;
            
        }
    }

    private string createNameForMethod(MethodBase method)
    {
        StringBuilder sb = new StringBuilder();
        string pattern = @"\(.*?\)";
        
        ParameterInfo[] cParams = method.GetParameters();
        sb.Append(Regex.Replace(method.ToString(), pattern, ""));
        sb.Append("(");
        for (int i = 0; i < cParams.Length; i++)
        {
            sb.Append(cParams[i]);
            if (i != cParams.Length - 1)
            {
                sb.Append(", ");
            }
        }
        sb.Append(")");
        return sb.ToString();
    }

    private CommandClass _createObjectCommand;

    public CommandClass CreateObjectCommand
    {
        get
        {
            return (_createObjectCommand = new CommandClass(o =>
                   {
                       Console.WriteLine("create");
                       Console.WriteLine((string) o);
                       string parametersStr = (string)o;
                       string[] parameters = parametersStr.Split(",");
                       _modelClass.ParamsConstructor = parameters.ToList();
                   }));
        }
    }
}
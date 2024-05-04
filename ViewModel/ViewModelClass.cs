using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Task3_1.Model;

namespace Task3_1.ViewModel;

class ViewModelClass
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
            } else if (e.PropertyName == nameof(modelClass.SelectedConstructor))
            {
                SelectedConstr = "";
            } else if (e.PropertyName == nameof(modelClass.SelectedMethod))
            {
                SelectedMethod = "";
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
            if (Equals(value, "")) return;
            _modelClass.SelectedConstructor = mapStringConstructorInfos[value];
        }
    }

    public string SelectedMethod
    {
        get => _selectedMethod;
        set
        {
            _selectedMethod = value;
            if (Equals(value, "")) return;
            _modelClass.SelectedMethod = mapStringMethodInfos[value];
        }
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
                       _modelClass.ParamsConstructor = getArgsFromTextBox(o).ToList();
                   }));
        }
    }
    
    private CommandClass _executeMethodCommand;
    public CommandClass ExecuteMethodCommand
    {
        get
        {
            return (_executeMethodCommand = new CommandClass(o =>
            {
                Console.WriteLine("execute");
                _modelClass.ParamsMethod = getArgsFromTextBox(o).ToList();
            }));
        }
    }

    private string[] getArgsFromTextBox(object o)
    {
        string parametersStr = (string)o;
        return parametersStr.Split(",");
    }
}
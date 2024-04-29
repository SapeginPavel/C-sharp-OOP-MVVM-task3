using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using Task3_1.Model;

namespace Task3_1.ViewModel;

class ViewModelClass : INotifyPropertyChanged
{
    private ObservableCollection<Type> _classes; //todo: упростить (сразу свойства сделать)
    private Type? _selectedClass;

    private ObservableCollection<MethodInfo> _methods;
    private MethodInfo? _selectedMethod;
    
    private ObservableCollection<string> _constructors;
    // private ObservableCollection<ConstructorInfo> _constructors;
    private ConstructorInfo? _selectedConstructor;
    
    private ObservableCollection<ParameterInfo> _paramsMethod;
    private ObservableCollection<ParameterInfo> _paramsConstructor;

    private Dictionary<string, ConstructorInfo> mapStringConstructorInfos;

    private ModelClass _modelClass;


    public ViewModelClass(ModelClass modelClass)
    {
        _modelClass = modelClass;
        Classes = new ObservableCollection<Type>();
        Methods = new ObservableCollection<MethodInfo>();
        // Constructors = new ObservableCollection<ConstructorInfo>();
        Constructors = new ObservableCollection<string>();
        ParamsMethod = new ObservableCollection<ParameterInfo>();
        ParamsConstructor = new ObservableCollection<ParameterInfo>();

        mapStringConstructorInfos = new Dictionary<string, ConstructorInfo>();
        
        modelClass.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(modelClass.Classes))
            {
                Classes.Clear();
                foreach (Type t in modelClass.Classes)
                {
                    Classes.Add(t);
                }
            } else if (e.PropertyName == nameof(modelClass.Methods))
            {
                Methods.Clear();
                foreach (MethodInfo methodInfo in modelClass.Methods)
                {
                    Methods.Add(methodInfo);
                }
            } else if (e.PropertyName == nameof(modelClass.Constructors))
            {/////////////////////////////////////////////////////////////////////////////
                Constructors.Clear();
                mapStringConstructorInfos.Clear();
                string name = "";
                foreach (var constructor in modelClass.Constructors)
                {
                    ParameterInfo[] cParams = constructor.GetParameters();
                    name = constructor.ToString();
                    foreach (var par in cParams)
                    {
                        name += " " + par;
                    }
                    Constructors.Add(name);
                    Console.WriteLine("Вот что: " + name);
                    mapStringConstructorInfos.Add(name, constructor);
                }
                
                // Constructors.Clear();
                // foreach (ConstructorInfo constructorInfo in modelClass.Constructors)
                // {
                //     Constructors.Add(constructorInfo);
                // }
                // Console.WriteLine("Создали");
                // OnPropertyChanged(nameof(Constructors));
                // Console.WriteLine("Активировали");
            } else if (e.PropertyName == nameof(modelClass.ParamsMethod))
            {
                ParamsMethod.Clear();
                foreach (ParameterInfo parameterInfo in modelClass.ParamsMethod)
                {
                    ParamsMethod.Add(parameterInfo);
                }
            } else if (e.PropertyName == nameof(modelClass.ParamsConstructor))
            {
                ParamsMethod.Clear();
                foreach (ParameterInfo parameterInfo in modelClass.ParamsConstructor)
                {
                    ParamsConstructor.Add(parameterInfo);
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

    public ObservableCollection<MethodInfo> Methods
    {
        get => _methods;
        set => _methods = value ?? throw new ArgumentNullException(nameof(value));
    }

    // public ObservableCollection<ConstructorInfo> Constructors
    // {
    //     get => _constructors;
    //     set => _constructors = value ?? throw new ArgumentNullException(nameof(value));
    // }

    public ObservableCollection<string> Constructors
    {
        get => _constructors;
        set => _constructors = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Dictionary<string, ConstructorInfo> MapStringConstructorInfos
    {
        get => mapStringConstructorInfos;
        set => mapStringConstructorInfos = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ObservableCollection<ParameterInfo> ParamsMethod
    {
        get => _paramsMethod;
        set => _paramsMethod = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ObservableCollection<ParameterInfo> ParamsConstructor
    {
        get => _paramsConstructor;
        set => _paramsConstructor = value ?? throw new ArgumentNullException(nameof(value));
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

    public MethodInfo? SelectedMethod
    {
        get => _selectedMethod;
        set
        {
            _modelClass.SelectedMethod = value;
            _selectedMethod = value;
        }
    }

    public ConstructorInfo? SelectedConstructor
    {
        get => _selectedConstructor;
        set
        {
            _modelClass.SelectedConstructor = value;
            _selectedConstructor = value;
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
}
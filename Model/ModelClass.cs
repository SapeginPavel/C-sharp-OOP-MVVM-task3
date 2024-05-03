using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Task3_1.Model;

public class ModelClass : INotifyPropertyChanged
{
    private string? _pathToAssembly;
    private Assembly? _assemblyLoaded;

    private List<Type> _classes;
    private Type? _selectedClass;
    
    public List<ConstructorInfo> Constructors { get; }
    private ConstructorInfo? _selectedConstructor;

    private List<MethodInfo> _methods;
    private MethodInfo? _selectedMethod;

    private List<string> _paramsConstructor;
    private List<string> _paramsMethod;

    private object createdObject;

    public ModelClass()
    {
        _classes = new List<Type>();
        _methods = new List<MethodInfo>();
        Constructors = new List<ConstructorInfo>();
        ParamsConstructor = new List<string>();
        ParamsMethod = new List<string>();
    }

    public string? PathToAssembly
    {
        get => _pathToAssembly;
        set
        {
            if (value == _pathToAssembly) return;
            _pathToAssembly = value;
            UpdateAssemblyLoaded();
            ReadClassesFromAssemblyLoaded();
            OnPropertyChanged(nameof(PathToAssembly));
        }
    }

    public Assembly? AssemblyLoaded
    {
        get => _assemblyLoaded;
        set
        {
            if (Equals(value, _assemblyLoaded)) return;
            _assemblyLoaded = value;
            OnPropertyChanged(nameof(AssemblyLoaded));
        }
    }

    public List<Type> Classes
    {
        get => _classes;
        set
        {
            if (Equals(value, _classes)) return;
            _classes = value;
            OnPropertyChanged(nameof(Classes));
        }
    }

    public List<MethodInfo> Methods
    {
        get => _methods;
        set
        {
            if (Equals(value, _methods)) return;
            _methods = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Methods));
        }
    }

    public List<string> ParamsMethod
    {
        get => _paramsMethod;
        set
        {
            if (Equals(value, _paramsMethod)) return;
            _paramsMethod = value;
            OnPropertyChanged();
        }
    }

    public List<string> ParamsConstructor
    {
        get => _paramsConstructor;
        set
        {
            if (Equals(value, _paramsConstructor)) return;
            _paramsConstructor = value;
            createObject();
            OnPropertyChanged();
        }
    }

    private void createObject()
    {
        if (SelectedConstructor == null)
        {
            return;
        }
        ParameterInfo[] constructorParams = SelectedConstructor.GetParameters();
        object[] paramsForConstructor = new object[constructorParams.Length];
        for (int i = 0; i < constructorParams.Length; i++)
        {
            paramsForConstructor[i] = Convert.ChangeType(ParamsConstructor[i], constructorParams[i].ParameterType);
        }
        createdObject = SelectedConstructor.Invoke(paramsForConstructor);
    }

    public Type? SelectedClass
    {
        get => _selectedClass;
        set
        {
            if (Equals(value, _selectedClass)) return;
            _selectedClass = value;
            ReadMethodsFromAssemblyLoaded();
            ReadConstructorsFromAssemblyLoaded();
        }
    }

    public MethodInfo? SelectedMethod
    {
        get => _selectedMethod;
        set
        {
            if (Equals(value, _selectedMethod)) return;
            _selectedMethod = value;
            OnPropertyChanged(nameof(SelectedMethod));
        }
    }

    public ConstructorInfo? SelectedConstructor
    {
        get => _selectedConstructor;
        set
        {
            if (Equals(value, _selectedConstructor)) return;
            _selectedConstructor = value;
        }
    }

    private void UpdateAssemblyLoaded()
    {
        AssemblyLoaded = Assembly.LoadFrom(PathToAssembly);
    }

    private void ReadClassesFromAssemblyLoaded()
    {
        Classes.Clear();
        Type[] classesTemp = AssemblyLoaded.GetTypes();
        foreach (var c in classesTemp)
        {
            if (c.IsInterface || c.IsAbstract)
            {
                continue;
            }
            Classes.Add(c);
        }
        OnPropertyChanged(nameof(Classes));
    }

    private void ReadMethodsFromAssemblyLoaded()
    {
        Methods.Clear();
        MethodInfo[] methodsTemp = SelectedClass.GetMethods();
        foreach (var m in methodsTemp)
        {
            if (m.IsConstructor || m.IsAbstract)
            {
                continue;
            }
            Methods.Add(m);
        }
        OnPropertyChanged(nameof(Methods));
    }
    
    private void ReadConstructorsFromAssemblyLoaded()
    {
        Constructors.Clear();
        ConstructorInfo[] constructorsTemp = SelectedClass.GetConstructors();
        foreach (var c in constructorsTemp)
        {
            Constructors.Add(c);
            Console.WriteLine(c.ToString());
        }
        OnPropertyChanged(nameof(Constructors));
    }

    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
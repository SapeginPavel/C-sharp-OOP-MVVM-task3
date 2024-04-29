using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Task3_1.Model;

public class ModelClass : INotifyPropertyChanged
{
    private string? _pathToAssembly;
    private Assembly? _assemblyLoaded;

    private List<Type> _classes;
    private Type? _selectedClass;

    private List<MethodInfo> _methods;
    private MethodInfo? _selectedMethod;
    
    private List<Type> _params;



    public ModelClass()
    {
        _classes = new List<Type>();
        _methods = new List<MethodInfo>();
        _params = new List<Type>();
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

    public Type? SelectedClass
    {
        get => _selectedClass;
        set
        {
            if (Equals(value, _selectedClass)) return;
            _selectedClass = value;
            ReadMethodsFromAssemblyLoaded();
            OnPropertyChanged(nameof(SelectedClass));
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

    private void UpdateAssemblyLoaded()
    {
        AssemblyLoaded = Assembly.LoadFrom(PathToAssembly);
    }

    private void ReadClassesFromAssemblyLoaded()
    {
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

    private void ReadMethodParamsFromAssemblyLoaded()
    {
        SelectedMethod.GetParameters();
    }
    
    
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
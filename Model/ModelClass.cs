using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Task3_1.Model;

public class ModelClass : INotifyPropertyChanged
{
    private string? _pathToAssembly;
    private Assembly? _assemblyLoaded;
    private Type? _selectedClass;
    private List<Type> _classes;


    public ModelClass()
    {
        _classes = null;
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

    public Type? SelectedClass
    {
        get => _selectedClass;
        set
        {
            if (Equals(value, _selectedClass)) return;
            _selectedClass = value;
            OnPropertyChanged(nameof(SelectedClass));
        }
    }

    private void UpdateAssemblyLoaded()
    {
        AssemblyLoaded = Assembly.LoadFrom(PathToAssembly);
    }

    private void ReadClassesFromAssemblyLoaded()
    {
        Classes = AssemblyLoaded.GetTypes().ToList();
    }
    
    
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
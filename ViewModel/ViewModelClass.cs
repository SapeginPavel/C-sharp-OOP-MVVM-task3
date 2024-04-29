using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Win32;
using Task3_1.Model;

namespace Task3_1.ViewModel;

class ViewModelClass : INotifyPropertyChanged
{
    private ObservableCollection<Type> _classes; //todo: упростить (сразу свойства сделать)
    private ObservableCollection<MethodInfo> _methods;

    private ModelClass _modelClass;

    private Type? _selectedClass;
    private MethodInfo? _selectedMethod;

    public ViewModelClass(ModelClass modelClass)
    {
        _modelClass = modelClass;
        Classes = new ObservableCollection<Type>();
        Methods = new ObservableCollection<MethodInfo>();
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
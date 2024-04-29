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
    private ObservableCollection<Type> _classes;
    // private string? _pathToAssembly;
    // private Assembly? _assemblyLoaded;

    private ModelClass _modelClass;

    public ViewModelClass(ModelClass modelClass)
    {
        _modelClass = modelClass;
        modelClass.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(modelClass.Classes))
            {
                Classes.Clear();
                foreach (Type t in modelClass.Classes)
                {
                    if (t.IsAbstract)
                    {
                        continue;
                    }
                    Classes.Add(t);
                }
                
            } else if (e.PropertyName == nameof(modelClass.AssemblyLoaded))
            {
                
            }
        };
    }

    public ObservableCollection<Type> Classes
    {
        get => _classes;
        set => _classes = value ?? throw new ArgumentNullException(nameof(value));
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
        if (openFileDialog.ShowDialog() == true)
        {
            string selectedFilePath = openFileDialog.FileName;
            _modelClass.PathToAssembly = selectedFilePath;
            
        }
    }

    
    
}
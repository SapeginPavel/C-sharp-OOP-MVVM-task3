using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Win32;
using Task3_1.Model;

namespace Task3_1.ViewModel;

class ViewModelClass : INotifyPropertyChanged
{
    // private string? _pathToAssembly;
    // private Assembly? _assemblyLoaded;

    private ModelClass _modelClass;

    public ViewModelClass(ModelClass modelClass)
    {
        _modelClass = modelClass;
        modelClass.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(modelClass.PathToAssembly))
            {
                
            } else if (e.PropertyName == nameof(modelClass.AssemblyLoaded))
            {
                MessageBox.Show("Работает");
            }
        };
    }


    // public string? PathToAssembly
    // {
    //     get => _pathToAssembly;
    //     set
    //     {
    //         if (value == _pathToAssembly) return;
    //         _pathToAssembly = value;
    //         // UpdateAssemblyLoaded();
    //         // OnPropertyChanged(nameof(PathToAssembly)); //todo: мб не нужно
    //     }
    // }
    //
    // public Assembly? AssemblyLoaded
    // {
    //     get => _assemblyLoaded;
    //     set
    //     {
    //         if (Equals(value, _assemblyLoaded)) return;
    //         _assemblyLoaded = value;
    //         MessageBox.Show(_pathToAssembly);
    //         // OnPropertyChanged();
    //     }
    // }

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
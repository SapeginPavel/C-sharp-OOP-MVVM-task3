using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace Task3_1.ViewModel;

class ViewModelClass : INotifyPropertyChanged
{
    private string? _pathToAssembly;
    private Assembly? _assemblyLoaded;
    

    public string? PathToAssembly
    {
        get => _pathToAssembly;
        set
        {
            if (value == _pathToAssembly) return;
            _pathToAssembly = value;
            UpdateAssemblyLoaded();
            // OnPropertyChanged(nameof(PathToAssembly)); //todo: мб не нужно
        }
    }

    public Assembly? AssemblyLoaded
    {
        get => _assemblyLoaded;
        set
        {
            if (Equals(value, _assemblyLoaded)) return;
            _assemblyLoaded = value;
            // OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
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
            PathToAssembly = selectedFilePath;
            
        }
    }

    private void UpdateAssemblyLoaded()
    {
        AssemblyLoaded = Assembly.LoadFrom(_pathToAssembly);
    }
    
}
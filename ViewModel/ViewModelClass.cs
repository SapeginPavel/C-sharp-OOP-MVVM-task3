using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace Task3_1.ViewModel;

class ViewModelClass : INotifyPropertyChanged
{
    private string? pathToClassLibrary;

    public string? PathToClassLibrary
    {
        get => pathToClassLibrary;
        set
        {
            if (value == pathToClassLibrary) return;
            pathToClassLibrary = value;
            OnPropertyChanged(nameof(PathToClassLibrary)); //todo: мб не нужно
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
            // return openFileCommand ??
            //        (openFileCommand = new CommandClass(obj =>
            //        {
            //            Console.WriteLine("rkgnr");
            //        }));
            return openFileCommand;
        }
    }
    
    private void OpenFileDialogAction(object parameter)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            // Код для обработки выбранного файла
            string selectedFilePath = openFileDialog.FileName;
            // Здесь вы можете выполнить необходимые действия с выбранным файлом
        }
    }
    
}
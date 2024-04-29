using System.ComponentModel;

namespace AirCraftsAssembly;

public abstract class AirCraft : INotifyPropertyChanged
{
    protected int maxHeightAboveGround; //максимальная высота, куда взлетаем
    protected int currentHeightAboveGround; //текущая высота
    protected int stepForHeight; //пока не нужен (шаг для набора высоты)

    public AirCraft(int maxHeightAboveGround, int stepForHeight)
    {
        this.maxHeightAboveGround = maxHeightAboveGround;
        this.stepForHeight = stepForHeight;
        currentHeightAboveGround = 0;
    }

    protected AirCraft(int maxHeightAboveGround)
    {
        this.maxHeightAboveGround = maxHeightAboveGround;
    }

    public int CurrentHeightAboveGround 
    { 
        get => currentHeightAboveGround;
        set 
        { 
            currentHeightAboveGround = value;
            NotifyPropertyChanged(nameof(CurrentHeightAboveGround));
        } 
    }

    public abstract bool Up();
    public abstract bool Down();


    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged(String propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
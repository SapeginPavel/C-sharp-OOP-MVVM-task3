namespace AirCraftsAssembly;

public class Helicopter : AirCraft
{
    public Helicopter(int maxHeightAboveGround) : base(maxHeightAboveGround)
    {
    }

    public override bool Down(int step)
    {
        if (CurrentHeightAboveGround > 0)
        {
            if (CurrentHeightAboveGround - step < 0)
            {
                CurrentHeightAboveGround = 0;
            }
            else
            {
                CurrentHeightAboveGround -= step;
            }
        }
        Console.WriteLine("Выполнен полёт вниз для вертолёта. Текущая высота: " + CurrentHeightAboveGround);
        return true;
    }

    public override bool Up(int step)
    {
        if (CurrentHeightAboveGround < maxHeightAboveGround)
        {
            if (CurrentHeightAboveGround + step > maxHeightAboveGround)
            {
                CurrentHeightAboveGround = maxHeightAboveGround;
            } else
            {
                CurrentHeightAboveGround += step;
            }
        }
        Console.WriteLine("Выполнен полёт вверх для вертолёта. Текущая высота: " + CurrentHeightAboveGround);
        return true;
    }
}
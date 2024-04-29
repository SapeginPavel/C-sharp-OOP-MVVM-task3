namespace AirCraftsAssembly;

internal class Helicopter : AirCraft
{
    public Helicopter(int maxHeightAboveGround, int stepForHeight) : base(maxHeightAboveGround, stepForHeight)
    {
    }

    public override bool Down()
    {
        if (CurrentHeightAboveGround > 0)
        {
            if (CurrentHeightAboveGround - stepForHeight < 0)
            {
                CurrentHeightAboveGround = 0;
            }
            else
            {
                CurrentHeightAboveGround -= stepForHeight;
            }
        }
        Console.WriteLine("Выполнен полёт вниз для вертолёта. Текущая высота: " + CurrentHeightAboveGround);
        return true;
    }

    public override bool Up()
    {
        if (CurrentHeightAboveGround < maxHeightAboveGround)
        {
            if (CurrentHeightAboveGround + stepForHeight > maxHeightAboveGround)
            {
                CurrentHeightAboveGround = maxHeightAboveGround;
            } else
            {
                CurrentHeightAboveGround += stepForHeight;
            }
        }
        Console.WriteLine("Выполнен полёт вверх для вертолёта. Текущая высота: " + CurrentHeightAboveGround);
        return true;
    }
}
namespace AirCraftsAssembly;

public class Airplane : AirCraft
    {
        private int requiredRunwayLength;
        private int currentRunwayLength;

        public Airplane(int maxHeightAboveGround, int requiredRunwayLength) : base(maxHeightAboveGround)
        {
            this.requiredRunwayLength = requiredRunwayLength;
        }

        public int CurrentRunwayLength
        {
            get
            {
                return currentRunwayLength;
            }
            set
            {
                currentRunwayLength = value;
            }
        }
        public override bool Down(int step)
        {
            if ((CurrentHeightAboveGround == 0 || CurrentHeightAboveGround - step <= 0) && CurrentRunwayLength < requiredRunwayLength)
                
            {
                Console.WriteLine("Нельзя выполнить полёт вниз для самолёта. Текущая высота: " + CurrentHeightAboveGround);
                return false;
            }
            else
            {
                if (CurrentHeightAboveGround > 0)
                {
                    if (CurrentHeightAboveGround - step > 0)
                    {
                        CurrentHeightAboveGround -= step;
                    }
                    else
                    {
                        CurrentHeightAboveGround = 0;
                    }
                }
                Console.WriteLine("Выполнен полёт вниз для самолёта. Текущая высота: " + CurrentHeightAboveGround);
                return true;
            }
        }

        public override bool Up(int step)
        {
            if (CurrentHeightAboveGround == 0 && CurrentRunwayLength < requiredRunwayLength)
            {
                Console.WriteLine("Нельзя выполнить полёт вверх для самолёта. Текущая высота: " + CurrentHeightAboveGround);
                return false;
            }
            else
            {
                if (CurrentHeightAboveGround < maxHeightAboveGround)
                {
                    if (CurrentHeightAboveGround + step > requiredRunwayLength)
                    {
                        CurrentHeightAboveGround = maxHeightAboveGround;
                    } else
                    {
                        CurrentHeightAboveGround += step;
                    }
                }
                Console.WriteLine("Выполнен полёт вверх для самолёта. Текущая высота: " + CurrentHeightAboveGround);
                return true;
            }
        }
    }
namespace AirCraftsAssembly;

internal class Airplane : AirCraft
    {
        private int requiredRunwayLength;
        private int currentRunwayLength;

        public Airplane(int maxHeightAboveGround, int stepForHeight, int requiredRunwayLength) : base(maxHeightAboveGround, stepForHeight)
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
        public override bool Down()
        {
            if ((CurrentHeightAboveGround == 0 || CurrentHeightAboveGround - stepForHeight <= 0) && CurrentRunwayLength < requiredRunwayLength)
            {
                return false;
            }
            else
            {
                if (CurrentHeightAboveGround > 0)
                {
                    if (CurrentHeightAboveGround - stepForHeight > 0)
                    {
                        CurrentHeightAboveGround -= stepForHeight;
                    }
                    else
                    {
                        CurrentHeightAboveGround = 0;
                    }
                }
                return true; //тут можно было бы сделать вероятность не 100%
            }
        }

        public override bool Up()
        {
            if (CurrentHeightAboveGround == 0 && CurrentRunwayLength < requiredRunwayLength)
            {
                return false;
            }
            else
            {
                if (CurrentHeightAboveGround < maxHeightAboveGround)
                {
                    if (CurrentHeightAboveGround + stepForHeight > requiredRunwayLength)
                    {
                        CurrentHeightAboveGround = maxHeightAboveGround;
                    } else
                    {
                        CurrentHeightAboveGround += stepForHeight;
                    }
                }
                return true;
            }
        }
    }
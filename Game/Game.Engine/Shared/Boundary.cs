using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Shared
{
    public class Boundary
    {
        public float TopY { get; private set; }
        public float RightX { get; private set; }
        public float BottomY { get; private set; }
        public float LeftX { get; private set; }

        public Boundary(float topY, float rightX, float bottomY, float leftX)
        {
            TopY = topY;
            RightX = rightX;
            BottomY = bottomY;
            LeftX = leftX;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Shared.Commands
{
    public class FireCommandOptions
    {
        public float StartX { get; set; }
        public float StartY { get; set; }
        public float Direction { get; set; }

        public FireCommandOptions(float startX, float startY, float direction)
        {
            StartX = startX;
            StartY = startY;
            Direction = direction;
        }
    }
}

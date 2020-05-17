using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Events
{
    public class MoveEvent
    {
        public float Direction { get; set; }

        public MoveEvent(float direction)
        {
            Direction = direction;
        }
    }
}

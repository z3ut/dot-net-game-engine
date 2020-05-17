using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Events
{
    public class IntersectionEvent
    {
        public IGameObject IntersectionObject { get; set; }

        public IntersectionEvent(IGameObject intersectionObject)
        {
            IntersectionObject = intersectionObject;
        }
    }
}

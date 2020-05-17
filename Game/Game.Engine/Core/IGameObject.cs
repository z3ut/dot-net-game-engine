using Game.Engine.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Core
{
    public interface IGameObject : ICloneable
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Azimuth { get; set; }
        public bool IsAbstract { get; set; }
        public Boundary Boundary { get; }

        void Init(IGameWorld gameWorld);
        void Update(IGameWorld gameWorld);
        void Render(IGameWorld gameWorld);
        void HandleEvent(IGameWorld gameWorld, object gameEvent);
        void Destroy(IGameWorld gameWorld);
    }
}

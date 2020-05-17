using Game.Engine.Components;
using Game.Engine.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Core
{
    public class GameObject : IGameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Azimuth { get; set; }
        public bool IsAbstract { get; set; }

        public Boundary Boundary
        {
            get
            {
                var topY = Y + (Height - 1) / 2;
                var rightX = X + (Width - 1) / 2;
                var bottomY = Y - (Height - 1) / 2;
                var leftX = X - (Width - 1) / 2;
                return new Boundary(topY, rightX, bottomY, leftX);
            }
        }

        private readonly IGameComponent _inputComponent;
        private readonly IGameComponent _physicComponent;
        private readonly IGameComponent _logicComponent;
        private readonly IGameComponent _graphicComponent;

        public GameObject(IGameComponent inputComponent, IGameComponent physicComponent,
            IGameComponent logicComponent, IGameComponent graphicComponent)
        {
            _inputComponent = inputComponent;
            _physicComponent = physicComponent;
            _logicComponent = logicComponent;
            _graphicComponent = graphicComponent;
        }

        public object Clone()
        {
            var gameObject = new GameObject(
                _inputComponent?.Clone() as IGameComponent,
                _physicComponent?.Clone() as IGameComponent,
                _logicComponent?.Clone() as IGameComponent,
                _graphicComponent?.Clone() as IGameComponent)
            {
                X = X,
                Y = Y,
                Width = Width,
                Height = Height,
                Azimuth = Azimuth,
                IsAbstract = IsAbstract
            };
            return gameObject;
        }

        public void Init(IGameWorld gameWorld)
        {
            _inputComponent?.Init(this, gameWorld);
            _physicComponent?.Init(this, gameWorld);
            _logicComponent?.Init(this, gameWorld);
            _graphicComponent?.Init(this, gameWorld);
        }

        public void Update(IGameWorld gameWorld)
        {
            _inputComponent?.Update(this, gameWorld);
            _physicComponent?.Update(this, gameWorld);
            _logicComponent?.Update(this, gameWorld);
        }

        public void Render(IGameWorld gameWorld)
        {
            _graphicComponent?.Update(this, gameWorld);
        }

        public void HandleEvent(IGameWorld gameWorld, object gameEvent)
        {
            _inputComponent?.HandleEvent(this, gameWorld, gameEvent);
            _physicComponent?.HandleEvent(this, gameWorld, gameEvent);
            _logicComponent?.HandleEvent(this, gameWorld, gameEvent);
            _graphicComponent?.HandleEvent(this, gameWorld, gameEvent);
        }

        public void Destroy(IGameWorld gameWorld)
        {
            _inputComponent?.Destroy(this, gameWorld);
            _physicComponent?.Destroy(this, gameWorld);
            _logicComponent?.Destroy(this, gameWorld);
            _graphicComponent?.Destroy(this, gameWorld);
        }
    }
}

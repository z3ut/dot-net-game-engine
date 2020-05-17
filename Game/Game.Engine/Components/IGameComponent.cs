using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Components
{
    public interface IGameComponent : ICloneable
    {
        void Init(IGameObject gameObject, IGameWorld gameWorld);
        void Update(IGameObject gameObject, IGameWorld gameWorld);
        void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent);
        void Destroy(IGameObject gameObject, IGameWorld gameWorld);
    }
}

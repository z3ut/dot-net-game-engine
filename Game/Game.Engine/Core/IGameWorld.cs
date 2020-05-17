using Game.Engine.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Core
{
    public interface IGameWorld
    {
        void Init();
        void Update();
        void Render();
        void SendEvent(object gameEvent);
        void Destroy();

        void AddGameObject(IGameObject gameObject);
        void Destroy(IGameObject gameObject);

        IGameObject FindIntersection(Boundary boundary, IEnumerable<IGameObject> excludeGameObjects);
        bool IsObjectsVisibleToEachOther(IGameObject gameObject1, IGameObject gameObject2);
        float GetDistance(IGameObject gameObject1, IGameObject gameObject2);

        delegate void GameEventHandler(object sender, object e);
        event GameEventHandler EventHandler;
    }
}

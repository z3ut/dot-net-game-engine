using Game.Engine.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Engine.Core
{
    public class GameWorld : IGameWorld
    {
        private readonly ICollection<IGameObject> _gameObjects = new List<IGameObject>();
        private readonly IGeometryMathService _geometryMathService;

        public event IGameWorld.GameEventHandler EventHandler;

        private IEnumerable<IGameObject> _physicalGameObjects
        {
            get
            {
                return _gameObjects.Where(go => !go.IsAbstract);
            }
        }

        public GameWorld(IGeometryMathService geometryMathService)
        {
            _geometryMathService = geometryMathService;
        }

        public void Destroy(IGameObject gameObject)
        {
            gameObject.Destroy(this);
            _gameObjects.Remove(gameObject);
        }

        public void Render()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Render(this);
            }
        }

        public void SendEvent(object gameEvent)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.HandleEvent(this, gameEvent);
            }

            EventHandler.Invoke(this, gameEvent);
        }

        public void Update()
        {
            var currentObjects = _gameObjects.ToList();
            foreach (var gameObject in currentObjects)
            {
                gameObject.Update(this);
            }
        }

        public IGameObject FindIntersection(Boundary boundary,
            IEnumerable<IGameObject> excludeGameObjects)
        {
            return _physicalGameObjects
                .Except(excludeGameObjects)
                .FirstOrDefault(go =>
                    _geometryMathService.IsIntersects(go.Boundary, boundary));
        }

        public void AddGameObject(IGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Init()
        {
            foreach (var go in _gameObjects)
            {
                go.Init(this);
            }
        }

        public void Destroy()
        {
            foreach (var go in _gameObjects)
            {
                go.Destroy(this);
            }
        }

        public float GetDistance(IGameObject gameObject1, IGameObject gameObject2)
        {
            return _geometryMathService.Distance(gameObject1.X, gameObject1.Y,
                gameObject2.X, gameObject2.Y);
        }

        public bool IsObjectsVisibleToEachOther(IGameObject gameObject1, IGameObject gameObject2)
        {
            foreach (var go in _physicalGameObjects
                .Where(g => g != gameObject1 && g != gameObject2))
            {
                if (_geometryMathService.IsIntersects(go.Boundary,
                    gameObject1.X, gameObject1.Y, gameObject2.X, gameObject2.Y))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

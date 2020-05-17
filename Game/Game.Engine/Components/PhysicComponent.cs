using Game.Engine.Commands;
using Game.Engine.Core;
using Game.Engine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Components
{
    public class PhysicComponent : IGameComponent
    {

        private readonly float _speed;

        public PhysicComponent(float speed)
        {
            _speed = speed;
        }

        public object Clone()
        {
            return new PhysicComponent(_speed);
        }

        public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
        {

        }

        public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
        {
            if (gameEvent is MoveEvent)
            {
                var moveEvent = gameEvent as MoveEvent;

                gameObject.Azimuth = moveEvent.Direction;

                var amountX = (float)Math.Sin(ConvertDegreeToRadian(moveEvent.Direction)) * _speed;
                var amountY = (float)Math.Cos(ConvertDegreeToRadian(moveEvent.Direction)) * _speed;

                var moveCommand = new MoveCommand();
                var moveCommandOptions = new MoveCommandOptions(amountX, amountY);
                moveCommand.Do(gameObject, gameWorld, moveCommandOptions);

                var movedEvent = new MovedEvent(gameObject.X, gameObject.Y);
                gameObject.HandleEvent(gameWorld, movedEvent);
            }
        }

        public void Init(IGameObject gameObject, IGameWorld gameWorld)
        {

        }

        public void Update(IGameObject gameObject, IGameWorld gameWorld)
        {

        }

        private double ConvertDegreeToRadian(float degree)
        {
            return Math.PI * degree / 180;
        }
    }
}

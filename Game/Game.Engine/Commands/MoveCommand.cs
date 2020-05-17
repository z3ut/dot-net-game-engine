using Game.Engine.Core;
using Game.Engine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Commands
{
    public class MoveCommandOptions
    {
        public float AmountX { get; set; }
        public float AmountY { get; set; }

        public MoveCommandOptions(float amountX, float amountY)
        {
            AmountX = amountX;
            AmountY = amountY;
        }
    }

    public class MoveCommand : IGameCommand
    {
        public void Do(IGameObject gameObject, IGameWorld gameWorld, object options)
        {
            var moveCommandOptions = options as MoveCommandOptions;

            var startX = gameObject.X;
            var startY = gameObject.Y;

            var endX = startX + moveCommandOptions.AmountX;
            var endY = startY + moveCommandOptions.AmountY;

            var pathLength = Math.Sqrt(Math.Pow(endX - startX, 2) + Math.Pow(endY - startY, 2));

            var numberOfSteps = (int)Math.Ceiling(pathLength);

            var deltaX = moveCommandOptions.AmountX / numberOfSteps;
            var deltaY = moveCommandOptions.AmountY / numberOfSteps;

            for (var i  = 0; i < numberOfSteps; i++)
            {
                gameObject.X += deltaX;
                gameObject.Y += deltaY;

                var intersection = gameWorld.FindIntersection(gameObject.Boundary,
                    new List<IGameObject>() { gameObject });

                if (intersection != null)
                {
                    intersection.HandleEvent(gameWorld, new IntersectionEvent(gameObject));
                    gameObject.HandleEvent(gameWorld, new IntersectionEvent(intersection));

                    gameObject.X -= deltaX;
                    gameObject.Y -= deltaY;
                    return;
                }
            }
        }
    }
}

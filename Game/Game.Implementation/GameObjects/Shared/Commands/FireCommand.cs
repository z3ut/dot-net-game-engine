using Game.Engine.Commands;
using Game.Engine.Components;
using Game.Engine.Core;
using Game.Implementation.GameObjects.Shell;
using Game.Implementation.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Shared.Commands
{
    public class FireCommand : IGameCommand
    {
        private readonly IGameObject _shell;

        public FireCommand(IGameObject shell)
        {
            _shell = shell;
        }

        public void Do(IGameObject gameObject, IGameWorld gameWorld, object options)
        {
            var fireCommandOptions = options as FireCommandOptions;

            var shell = _shell.Clone() as IGameObject;
            shell.X = fireCommandOptions.StartX;
            shell.Y = fireCommandOptions.StartY;
            shell.Azimuth = fireCommandOptions.Direction;

            gameWorld.AddGameObject(shell);
            shell.Update(gameWorld);
        }
    }
}

using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Commands
{
    public interface IGameCommand
    {
        void Do(IGameObject gameObject, IGameWorld gameWorld, object options);
    }
}

using Game.Engine.Core;
using Game.Implementation.GameObjects.Enemy.InputStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy.LogicStates
{
	public interface IEnemyLogicState
	{
		void Init(IGameObject gameObject, IGameWorld gameWorld);
		IEnemyLogicState Update(IGameObject gameObject, IGameWorld gameWorld);
		IEnemyLogicState HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent);
	}
}

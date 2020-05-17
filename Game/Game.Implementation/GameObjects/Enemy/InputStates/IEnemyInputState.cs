using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy.InputStates
{
	public interface IEnemyInputState
	{
		void Init(IGameObject gameObject, IGameWorld gameWorld);
		IEnemyInputState Update(IGameObject gameObject, IGameWorld gameWorld);
		IEnemyInputState HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent);
	}
}

using Game.Engine.Core;
using Game.Implementation.GameObjects.Enemy.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy.InputStates
{
	public class EnemyInputDestructionState : IEnemyInputState
	{
		public EnemyInputDestructionState()
		{

		}

		public IEnemyInputState HandleEvent(IGameObject gameObject,
			IGameWorld gameWorld, object gameEvent)
		{
			return this;
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public IEnemyInputState Update(IGameObject gameObject, IGameWorld gameWorld)
		{
			return this;
		}
	}
}

using Game.Engine.Core;
using Game.Implementation.GameObjects.Enemy.Events;
using Game.Implementation.GameObjects.Enemy.InputStates;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy.LogicStates
{
	public class EnemyLogicDestructionState : IEnemyLogicState
	{
		private readonly int _updatesTillDestroyed;
		private int _updatesPassed = 0;

		public EnemyLogicDestructionState(EnemyData enemyData)
		{
			_updatesTillDestroyed = enemyData.UpdatesTillDestroyed;
		}

		public IEnemyLogicState HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
		{
			if (gameEvent is EnemyDestructionEnd)
			{
				gameWorld.Destroy(gameObject);
			}

			return this;
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{
			gameObject.HandleEvent(gameWorld, new EnemyDestructionStart());
		}

		public IEnemyLogicState Update(IGameObject gameObject, IGameWorld gameWorld)
		{
			if (_updatesPassed > _updatesTillDestroyed)
			{
				gameObject.HandleEvent(gameWorld, new EnemyDestructionEnd());
			}

			_updatesPassed++;

			return this;
		}
	}
}

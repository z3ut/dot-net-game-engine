using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.Enemy.Events;
using Game.Implementation.GameObjects.Shell;
using Game.Implementation.Interactions;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy.InputStates
{
	public class EnemyInputIdleState : IEnemyInputState
	{
		private readonly IGameObjectLocator _gameObjectLocator;
		private readonly IGeometryMathService _geometryMathService;
		private readonly EnemyData _enemyData;
		private readonly float _playerAggroDistance;

		public EnemyInputIdleState(IGameObjectLocator gameObjectLocator,
			IGeometryMathService geometryMathService, EnemyData enemyData)
		{
			_gameObjectLocator = gameObjectLocator;
			_geometryMathService = geometryMathService;
			_enemyData = enemyData;
			_playerAggroDistance = enemyData.PlayerAggroDistance;
		}

		public IEnemyInputState HandleEvent(IGameObject gameObject,
			IGameWorld gameWorld, object gameEvent)
		{
			if (gameEvent is EnemyDestructionStart)
			{
				return new EnemyInputDestructionState();
			}

			return this;
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public IEnemyInputState Update(IGameObject gameObject, IGameWorld gameWorld)
		{
			var player = _gameObjectLocator.GetPlayer();

			var distanceToPlayer = gameWorld.GetDistance(gameObject, player);

			if (distanceToPlayer < _playerAggroDistance &&
				gameWorld.IsObjectsVisibleToEachOther(gameObject, player))
			{
				return new EnemyInputAggroState(_gameObjectLocator,
					_geometryMathService, _enemyData);
			}

			return this;
		}
	}
}

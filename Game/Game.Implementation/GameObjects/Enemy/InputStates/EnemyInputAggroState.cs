using Game.Engine.Core;
using Game.Engine.Events;
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

	public class EnemyInputAggroState : IEnemyInputState
	{
		private readonly IGameObjectLocator _gameObjectLocator;
		private readonly IGeometryMathService _geometryMathService;

		private readonly float _approachingDistance;
		private readonly float _playerLoseDistance;

		private readonly EnemyData _enemyData;

		public EnemyInputAggroState(IGameObjectLocator gameObjectLocator,
			IGeometryMathService geometryMathService, EnemyData enemyData)
		{
			_gameObjectLocator = gameObjectLocator;
			_geometryMathService = geometryMathService;

			_enemyData = enemyData;
			_approachingDistance = enemyData.ApproachingDistance;
			_playerLoseDistance = enemyData.PlayerLoseDistance;
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

			if (distanceToPlayer > _playerLoseDistance ||
				!gameWorld.IsObjectsVisibleToEachOther(gameObject, player))
			{
				return new EnemyInputIdleState(_gameObjectLocator,
					_geometryMathService, _enemyData);
			}

			if (distanceToPlayer > _approachingDistance)
			{
				var moveAzimuth = _geometryMathService.Azimuth(
					gameObject.X, gameObject.Y, player.X, player.Y);
				gameObject.HandleEvent(gameWorld, new MoveEvent(moveAzimuth));
			}

			var fireAzimuth = _geometryMathService.Azimuth(
				gameObject.X, gameObject.Y, player.X, player.Y);
			gameObject.HandleEvent(gameWorld, new FireEvent(fireAzimuth));

			return this;
		}
	}
}

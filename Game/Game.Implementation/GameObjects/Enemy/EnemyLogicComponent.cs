using Game.Engine.Commands;
using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.Enemy.Events;
using Game.Implementation.GameObjects.Enemy.InputStates;
using Game.Implementation.GameObjects.Enemy.LogicStates;
using Game.Implementation.GameObjects.Shared.Commands;
using Game.Implementation.GameObjects.Shell;
using Game.Implementation.Interactions;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy
{
	public class EnemyLogicComponent : IGameComponent
	{
		private IEnemyLogicState _enemyState { get; set; }

		private readonly IGameObjectLocator _gameObjectLocator;
		private readonly IGameCommand _fireCommand;
		private readonly IGeometryMathService _geometryMathService;
		private readonly EnemyData _enemyData;

		public EnemyLogicComponent(IGameObjectLocator gameObjectLocator,
			IGameCommand fireCommand, IGeometryMathService geometryMathService,
			EnemyData enemyData)
		{
			_enemyState = new EnemyLogicNormalState(gameObjectLocator,
				fireCommand, geometryMathService, enemyData);

			_gameObjectLocator = gameObjectLocator;
			_fireCommand = fireCommand;
			_geometryMathService = geometryMathService;
			_enemyData = enemyData;
		}

		public object Clone()
		{
			return new EnemyLogicComponent(_gameObjectLocator,
				_fireCommand, _geometryMathService, _enemyData);
		}

		public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
		{
			_gameObjectLocator.RemoveEnemy(gameObject);
		}

		public void HandleEvent(IGameObject gameObject,
			IGameWorld gameWorld, object gameEvent)
		{
			_enemyState = _enemyState.HandleEvent(gameObject, gameWorld, gameEvent);
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{
			_enemyState.Init(gameObject, gameWorld);
		}

		public void Update(IGameObject gameObject, IGameWorld gameWorld)
		{
			_enemyState = _enemyState.Update(gameObject, gameWorld);
		}
	}
}

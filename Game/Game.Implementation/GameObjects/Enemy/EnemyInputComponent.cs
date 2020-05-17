using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.Enemy.InputStates;
using Game.Implementation.Interactions;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy
{
	public class EnemyInputComponent : IGameComponent
	{
		private IEnemyInputState _enemyState { get; set; }

		private readonly IGameObjectLocator _gameObjectLocator;
		private readonly IGeometryMathService _geometryMathService;
		private readonly EnemyData _enemyData;

		public EnemyInputComponent(IGameObjectLocator gameObjectLocator,
			IGeometryMathService geometryMathService, EnemyData enemyData)
		{
			_enemyState = new EnemyInputIdleState(gameObjectLocator,
				geometryMathService, enemyData);

			_gameObjectLocator = gameObjectLocator;
			_geometryMathService = geometryMathService;
			_enemyData = enemyData;
		}

		public object Clone()
		{
			return new EnemyInputComponent(_gameObjectLocator,
				_geometryMathService, _enemyData);
		}

		public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
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

using Game.Engine.Commands;
using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.Enemy.Events;
using Game.Implementation.GameObjects.Enemy.InputStates;
using Game.Implementation.GameObjects.Shared.Commands;
using Game.Implementation.GameObjects.Shell;
using Game.Implementation.Interactions;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy.LogicStates
{
	public class EnemyLogicNormalState : IEnemyLogicState
	{
		private readonly IGameObjectLocator _gameObjectLocator;
		private readonly IGameCommand _fireCommand;
		private readonly IGeometryMathService _geometryMathService;
		private readonly EnemyData _enemyData;

		private readonly int _updatesForReload;
		private int _currentUpdatesForReload;

		public EnemyLogicNormalState(IGameObjectLocator gameObjectLocator,
			IGameCommand fireCommand, IGeometryMathService geometryMathService,
			EnemyData enemyData)
		{
			_gameObjectLocator = gameObjectLocator;
			_fireCommand = fireCommand;
			_geometryMathService = geometryMathService;
			_enemyData = enemyData;

			_updatesForReload = enemyData.UpdatesForReload;
			_currentUpdatesForReload = 0;
		}

		public IEnemyLogicState HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
		{
			if (gameEvent is ShellHitEvent)
			{
				var destructionState = new EnemyLogicDestructionState(_enemyData);
				destructionState.Init(gameObject, gameWorld);
				return destructionState;
			}

			if (gameEvent is FireEvent)
			{
				if (_currentUpdatesForReload == 0)
				{
					var fireEvent = gameEvent as FireEvent;

					_currentUpdatesForReload = _updatesForReload;

					var (fireX, fireY) = _geometryMathService
						.PointOnBoundary(gameObject, fireEvent.Direction);

					var fireCommandOptions =
						new FireCommandOptions(fireX, fireY, fireEvent.Direction);
					_fireCommand.Do(gameObject, gameWorld, fireCommandOptions);
				}
			}

			return this;
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public IEnemyLogicState Update(IGameObject gameObject, IGameWorld gameWorld)
		{
			if (_currentUpdatesForReload > 0)
			{
				_currentUpdatesForReload--;
			}

			return this;
		}
	}
}

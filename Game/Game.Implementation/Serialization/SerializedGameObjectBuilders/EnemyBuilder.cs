using Game.Engine.Commands;
using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.Enemy;
using Game.Implementation.GameObjects.Shared;
using Game.Implementation.Graphics;
using Game.Implementation.Interactions;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization.SerializedGameObjectBuilders
{
	public class EnemyBuilder : ISerializedGameObjectBuilder
	{
		public string SerializedName => GameObjectNameConstants.Enemy;

		private readonly IConsoleWriter _consoleWriter;
		private readonly IGameObjectLocator _gameObjectLocator;
		private readonly IGameCommand _fireCommand;
		private readonly IGeometryMathService _geometryMathService;
		private readonly EnemyData _enemyData;

		public EnemyBuilder(IConsoleWriter consoleWriter,
			IGameObjectLocator gameObjectLocator,
			IGeometryMathService geometryMathService, IGameCommand fireCommand,
			EnemyData enemyData)
		{
			_consoleWriter = consoleWriter;
			_gameObjectLocator = gameObjectLocator;
			_geometryMathService = geometryMathService;
			_fireCommand = fireCommand;
			_enemyData = enemyData;
		}

		public IGameObject BuildGameObject(GameObjectSerialized gameObjectSerialized)
		{
			if (gameObjectSerialized.GameObjectType != SerializedName)
			{
				throw new ArgumentException("Wrong serialized object");
			}

			var enemyInputComponent = new EnemyInputComponent(_gameObjectLocator,
				_geometryMathService, _enemyData);

			var enemyPhysicComponent = new PhysicComponent(_enemyData.Speed);

			var enemyLogicComponent = new EnemyLogicComponent(_gameObjectLocator,
				_fireCommand, _geometryMathService, _enemyData);

			var enemyGraphicComponent = new EnemyGraphicComponent(_consoleWriter,
				_enemyData.DisplayCharNormalState, _enemyData.DisplayCharDestructionState);

			var enemy = new GameObject(enemyInputComponent,
				enemyPhysicComponent, enemyLogicComponent, enemyGraphicComponent);
			enemy.X = gameObjectSerialized.X;
			enemy.Y = gameObjectSerialized.Y;

			enemy.Width = _enemyData.Width;
			enemy.Height = _enemyData.Height;

			return enemy;
		}
	}
}

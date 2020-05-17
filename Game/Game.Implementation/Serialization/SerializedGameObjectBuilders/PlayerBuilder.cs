using Game.Engine.Commands;
using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.Player;
using Game.Implementation.GameObjects.Shared;
using Game.Implementation.GameObjects.Shared.Commands;
using Game.Implementation.GameObjects.Shell;
using Game.Implementation.Graphics;
using Game.Implementation.Interactions;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization.SerializedGameObjectBuilders
{
	public class PlayerBuilder : ISerializedGameObjectBuilder
	{
		public string SerializedName => GameObjectNameConstants.Player;

		private readonly IConsoleWriter _consoleWriter;
		private readonly IGeometryMathService _geometryMathService;
		private readonly IGameCommand _fireCommand;
		private readonly PlayerData _playerData;

		public PlayerBuilder(IConsoleWriter consoleWriter,
			IGeometryMathService geometryMathService, IGameCommand fireCommand,
			PlayerData playerData)
		{
			_consoleWriter = consoleWriter;
			_geometryMathService = geometryMathService;
			_fireCommand = fireCommand;
			_playerData = playerData;
		}

		public IGameObject BuildGameObject(GameObjectSerialized gameObjectSerialized)
		{
			if (gameObjectSerialized.GameObjectType != SerializedName)
			{
				throw new ArgumentException("Wrong serialized object");
			}

			var playerInputComponent = new PlayerInputComponent();
			var playerPhysicComponent = new PhysicComponent(_playerData.Speed);
			var playerLogicComponent = new PlayerLogicComponent(_fireCommand, _consoleWriter,
				_geometryMathService);

			var playerGraphicComponent = new PlayerGraphicComponent(_consoleWriter,
				_geometryMathService, _playerData);

			var user = new GameObject(playerInputComponent,
				playerPhysicComponent, playerLogicComponent, playerGraphicComponent);
			user.X = gameObjectSerialized.X;
			user.Y = gameObjectSerialized.Y;

			user.Width = _playerData.Width;
			user.Height = _playerData.Height;

			return user;
		}
	}
}

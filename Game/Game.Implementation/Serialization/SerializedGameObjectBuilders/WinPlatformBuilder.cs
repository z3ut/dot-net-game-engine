using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.Shared;
using Game.Implementation.GameObjects.WinPlatform;
using Game.Implementation.Graphics;
using Game.Implementation.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization.SerializedGameObjectBuilders
{
	public class WinPlatformBuilder : ISerializedGameObjectBuilder
	{
		public string SerializedName  => GameObjectNameConstants.WinPlatform;

		private readonly IConsoleWriter _consoleWriter;
		private readonly IGameObjectLocator _gameObjectLocator;
		private readonly IGeometryMathService _geometryMathService;

		public WinPlatformBuilder(IConsoleWriter consoleWriter,
			IGameObjectLocator gameObjectLocator, IGeometryMathService geometryMathService)
		{
			_consoleWriter = consoleWriter;
			_gameObjectLocator = gameObjectLocator;
			_geometryMathService = geometryMathService;
		}

		public IGameObject BuildGameObject(GameObjectSerialized gameObjectSerialized)
		{
			if (gameObjectSerialized.GameObjectType != SerializedName)
			{
				throw new ArgumentException("Wrong serialized object");
			}

			var winPlatformLogicComponent = new WinPlatformLogicComponent(_gameObjectLocator,
				_geometryMathService);

			var winPlatformGraphicComponent = new CharGraphicComponent('w', _consoleWriter);

			var winPlatform = new GameObject(null,
				null, winPlatformLogicComponent, winPlatformGraphicComponent);
			winPlatform.X = gameObjectSerialized.X;
			winPlatform.Y = gameObjectSerialized.Y;
			winPlatform.Width = gameObjectSerialized.Width;
			winPlatform.Height = gameObjectSerialized.Height;
			winPlatform.IsAbstract = true;

			return winPlatform;
		}
	}
}

using Game.Engine.Core;
using Game.Implementation.GameObjects.Shared;
using Game.Implementation.Graphics;
using Game.Implementation.Interactions;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization.SerializedGameObjectBuilders
{
	public class StoneBuilder : ISerializedGameObjectBuilder
	{
		public string SerializedName => GameObjectNameConstants.Stone;

		private readonly IConsoleWriter _consoleWriter;
		private readonly StoneData _stoneData;

		public StoneBuilder(IConsoleWriter consoleWriter, StoneData stoneData)
		{
			_consoleWriter = consoleWriter;
			_stoneData = stoneData;
		}

		public IGameObject BuildGameObject(GameObjectSerialized gameObjectSerialized)
		{
			if (gameObjectSerialized.GameObjectType != SerializedName)
			{
				throw new ArgumentException("Wrong serialized object");
			}

			var stoneGraphicComponent = new CharGraphicComponent(_stoneData.DisplayChar,
				_consoleWriter);
			var stone = new GameObject(null, null, null, stoneGraphicComponent);
			stone.X = gameObjectSerialized.X;
			stone.Y = gameObjectSerialized.Y;
			stone.Width = gameObjectSerialized.Width;
			stone.Height = gameObjectSerialized.Height;

			return stone;
		}
	}
}

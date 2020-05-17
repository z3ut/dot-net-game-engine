using Game.Engine.Commands;
using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.Graphics;
using Game.Implementation.Interactions;
using Game.Implementation.Serialization.SerializedGameObjectBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Implementation.Serialization
{
	public class GameObjectBuilder : IGameObjectBuilder
	{
		private readonly IEnumerable<ISerializedGameObjectBuilder> _serializedGameObjectBuilders;
		private readonly IGameObjectTypeSaver _gameObjectTypeSaver;

		public GameObjectBuilder(
			IEnumerable<ISerializedGameObjectBuilder> serializedGameObjectBuilders,
			IGameObjectTypeSaver gameObjectTypeSaver)
		{
			_serializedGameObjectBuilders = serializedGameObjectBuilders;
			_gameObjectTypeSaver = gameObjectTypeSaver;
		}


		public IGameObject BuildGameObject(GameObjectSerialized gameObjectSerialized)
		{
			var objectBuilder = _serializedGameObjectBuilders
				.FirstOrDefault(b => b.SerializedName == gameObjectSerialized.GameObjectType);

			if (objectBuilder == null)
			{
				throw new ArgumentException("Unknown serialized object type");
			}

			var gameObject = objectBuilder.BuildGameObject(gameObjectSerialized);
			_gameObjectTypeSaver.SaveGameObject(gameObject, gameObjectSerialized.GameObjectType);
			return gameObject;
		}
	}
}

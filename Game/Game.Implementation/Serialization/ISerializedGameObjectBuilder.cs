using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization
{
	public interface ISerializedGameObjectBuilder
	{
		string SerializedName { get; }
		IGameObject BuildGameObject(GameObjectSerialized gameObjectSerialized);
	}
}

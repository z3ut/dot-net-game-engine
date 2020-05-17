using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization
{
	public interface IGameObjectBuilder
	{
		IGameObject BuildGameObject(GameObjectSerialized gameObjectSerialized);
	}
}

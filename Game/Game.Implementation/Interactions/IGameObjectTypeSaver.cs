using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Interactions
{
	public interface IGameObjectTypeSaver
	{
		void SaveGameObject(IGameObject gameObject, string serializedName);
	}
}

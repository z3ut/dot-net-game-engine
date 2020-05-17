using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Interactions
{
	public interface IGameObjectLocator
	{
		IGameObject GetPlayer();
		IEnumerable<IGameObject> GetEnemies();
		void RemoveEnemy(IGameObject enemy);
	}
}

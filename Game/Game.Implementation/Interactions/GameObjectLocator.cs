using Game.Engine.Core;
using Game.Implementation.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Interactions
{
	public class GameObjectLocator : IGameObjectLocator, IGameObjectTypeSaver
	{
		private IGameObject _player;
		private readonly IList<IGameObject> _enemies;

		public GameObjectLocator()
		{
			_enemies = new List<IGameObject>();
		}

		public IEnumerable<IGameObject> GetEnemies()
		{
			return _enemies;
		}

		public IGameObject GetPlayer()
		{
			return _player;
		}

		public void SaveEnemy(IGameObject gameObject)
		{
			_enemies.Add(gameObject);
		}

		public void RemoveEnemy(IGameObject gameObject)
		{
			_enemies.Remove(gameObject);
		}

		public void SaveGameObject(IGameObject gameObject, string serializedName)
		{
			switch (serializedName)
			{
				case GameObjectNameConstants.Player:
					SavePlayer(gameObject);
					return;
				case GameObjectNameConstants.Enemy:
					SaveEnemy(gameObject);
					return;
			}
		}

		public void SavePlayer(IGameObject gameObject)
		{
			_player = gameObject;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization
{
	public class WorldSerialized
	{
		public int WorldId { get; set; }
		public string Name { get; set; }

		public IEnumerable<GameObjectSerialized> GameObjects { get; set; }
	}
}

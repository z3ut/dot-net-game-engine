using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization.SerializedGameObjectData
{
	public class EnemyData
	{
		public int UpdatesTillDestroyed { get; set; }
		public int PlayerAggroDistance { get; set; }
		public int ApproachingDistance { get; set; }
		public int PlayerLoseDistance { get; set; }
		public int UpdatesForReload { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }
		public int Speed { get; set; }

		public char DisplayCharNormalState { get; set; }
		public char DisplayCharDestructionState { get; set; }
	}
}

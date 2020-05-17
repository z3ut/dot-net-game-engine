using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Serialization
{
	public interface ISerializedGameObjectDataProvider
	{
		EnemyData GetEnemyData();
		PlayerData GetPlayerData();
		ShellData GetShellData();
		StoneData GetStoneData();
	}
}

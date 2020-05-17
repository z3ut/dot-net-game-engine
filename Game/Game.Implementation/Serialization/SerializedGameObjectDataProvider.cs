using Game.Implementation.Serialization.SerializedGameObjectData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game.Implementation.Serialization
{
	public class SerializedGameObjectDataProvider : ISerializedGameObjectDataProvider
	{
		public EnemyData GetEnemyData()
		{
			return GetGameObjectData<EnemyData>("Enemy");
		}

		public PlayerData GetPlayerData()
		{
			return GetGameObjectData<PlayerData>("Player");
		}

		public ShellData GetShellData()
		{
			return GetGameObjectData<ShellData>("Shell");
		}

		public StoneData GetStoneData()
		{
			return GetGameObjectData<StoneData>("Stone");
		}

		private T GetGameObjectData<T>(string fileName)
		{
			var path = $"SerializedData/GameObjects/{fileName}.json";
			var json = File.ReadAllText(path);

			var gameObjectData = JsonConvert.DeserializeObject<T>(json);
			return gameObjectData;
		}
	}
}

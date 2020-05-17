using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.GameObjects.GameResult;
using Game.Implementation.GameObjects.Player;
using Game.Implementation.GameObjects.Shared;
using Game.Implementation.GameObjects.Shared.Commands;
using Game.Implementation.GameObjects.Shell;
using Game.Implementation.Graphics;
using Game.Implementation.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Game.Implementation.Worlds
{
	public class WorldProvider : IWorldProvider
	{
        private readonly IGameObjectBuilder _gameObjectBuilder;
        private readonly IGeometryMathService _geometryMathService;
        private readonly IConsoleWriter _consoleWriter;

        public WorldProvider(IGameObjectBuilder gameObjectBuilder,
            IGeometryMathService geometryMathService, IConsoleWriter consoleWriter)
        {
            _gameObjectBuilder = gameObjectBuilder;
            _geometryMathService = geometryMathService;
            _consoleWriter = consoleWriter;
        }

        public IGameWorld GetWorld(int worldDescriptionId)
		{
            var world = new GameWorld(_geometryMathService);

            var path = "SerializedData/Worlds/1.json";
            var json = File.ReadAllText(path);

            var worldSerialized = JsonConvert.DeserializeObject<WorldSerialized>(json);

            var gameObjects = worldSerialized
                .GameObjects.Select(sgo => _gameObjectBuilder.BuildGameObject(sgo));

            foreach (var go in gameObjects)
            {
                world.AddGameObject(go);
            }

            var gameResultGraphicComponent = new GameResultGraphicComponent(_consoleWriter);
            var gameResultLogicComponent = new GameResultLogicComponent();
            var gameResultComponent = new GameObject(null, null,
                gameResultLogicComponent, gameResultGraphicComponent);
            gameResultComponent.IsAbstract = true;
            world.AddGameObject(gameResultComponent);

            return world;
        }

		public IEnumerable<WorldDescription> GetWorldDescriptions()
		{
			return new List<WorldDescription>()
			{
				new WorldDescription() { WorldId = 1, Name = "World 1" }
			};
		}
	}
}

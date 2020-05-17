using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Events;
using Game.Engine.Shared;
using Game.Engine.Shared.Intersections;
using Game.Implementation.GameObjects.Player;
using Game.Implementation.GameObjects.Shared;
using Game.Implementation.GameObjects.Shared.Commands;
using Game.Implementation.GameObjects.Shell;
using Game.Implementation.Graphics;
using Game.Implementation.Interactions;
using Game.Implementation.Worlds;
using Game.Implementation.Serialization;
using Game.Implementation.Serialization.SerializedGameObjectBuilders;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;

namespace Game.Implementation
{
    class Program
    {
        private static bool IsGameRunning = true;

        static void Main(string[] args)
        {
            var consoleWriter = new ConsoleWriter(80, 40, 10, 0, 0);

            var world = CreateWorld(consoleWriter);

            world.EventHandler += World_EventHandler;

            world.Init();

            while (IsGameRunning)
            {
                world.Update();
                consoleWriter.BeginRender();
                world.Render();
                consoleWriter.EndRender();
                Thread.Sleep(100);
            }

            Console.ReadKey(true);
        }

        private static IGameWorld CreateWorld(IConsoleWriter consoleWriter)
        {
            var gameObjectLocator = new GameObjectLocator();

            var lineIntersectionResolver = new LianBarskyIntersectionResolver();
            var geometryMathService = new GeometryMathService(lineIntersectionResolver);

            var serializedGameObjectDataProvider = new SerializedGameObjectDataProvider();
            var enemyData = serializedGameObjectDataProvider.GetEnemyData();
            var playerData = serializedGameObjectDataProvider.GetPlayerData();
            var stoneData = serializedGameObjectDataProvider.GetStoneData();
            var shellData = serializedGameObjectDataProvider.GetShellData();

            var shellInputComponent = new ShellInputComponent();
            var shellPhysicComponent = new PhysicComponent(shellData.Speed);
            var shellLogicComponent = new ShellLogicComponent();
            var shellGraphicComponent = new CharGraphicComponent(shellData.DisplayChar, consoleWriter);
            var shell = new GameObject(shellInputComponent, shellPhysicComponent,
                shellLogicComponent, shellGraphicComponent);
            shell.Width = shellData.Width;
            shell.Height = shellData.Height;
            var fireCommand = new FireCommand(shell);

            var serializedGameObjectBuilders = new List<ISerializedGameObjectBuilder>()
            {
                new StoneBuilder(consoleWriter, stoneData),
                new PlayerBuilder(consoleWriter, geometryMathService, fireCommand, playerData),
                new EnemyBuilder(consoleWriter, gameObjectLocator, geometryMathService, fireCommand, enemyData),
                new WinPlatformBuilder(consoleWriter, gameObjectLocator, geometryMathService)
            };

            var gameObjectBuilder = new GameObjectBuilder(serializedGameObjectBuilders, gameObjectLocator);

            var worldProvider = new WorldProvider(gameObjectBuilder, geometryMathService, consoleWriter);
            var world = worldProvider.GetWorld(1);

            return world;
        }

        private static void World_EventHandler(object sender, object e)
        {
            if (e is GameEnd)
            {
                IsGameRunning = false;
            }
        }
    }
}

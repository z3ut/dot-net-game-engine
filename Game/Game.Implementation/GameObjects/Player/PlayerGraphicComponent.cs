using Game.Engine.Components;
using Game.Engine.Core;
using Game.Engine.Shared;
using Game.Implementation.Graphics;
using Game.Implementation.Serialization.SerializedGameObjectData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Player
{
    public class PlayerGraphicComponent : IGameComponent
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IGeometryMathService _geometryMathService;
        private readonly PlayerData _playerData;

        public PlayerGraphicComponent(IConsoleWriter consoleWriter,
            IGeometryMathService geometryMathService, PlayerData playerData)
        {
            _consoleWriter = consoleWriter;
            _geometryMathService = geometryMathService;
            _playerData = playerData;
        }

        public object Clone()
        {
            return new PlayerGraphicComponent(_consoleWriter,
                _geometryMathService, _playerData);
        }

        public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
        {
        }

        public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
        {
        }

        public void Init(IGameObject gameObject, IGameWorld gameWorld)
        {
        }

        public void Update(IGameObject gameObject, IGameWorld gameWorld)
        {
            var quarter = _geometryMathService.DegreeToQuarter(gameObject.Azimuth);
            var character = (quarter) switch
            {
                0 => _playerData.DisplayCharTop,
                1 => _playerData.DisplayCharRight,
                2 => _playerData.DisplayCharBot,
                3 => _playerData.DisplayCharLeft,
                _ => throw new Exception("Wrong quarter number"),
            };
            _consoleWriter.PrintChar(gameObject.X, gameObject.Y, character, 100);
        }
    }
}

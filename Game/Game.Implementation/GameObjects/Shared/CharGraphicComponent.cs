using Game.Engine.Components;
using Game.Engine.Core;
using Game.Implementation.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Shared
{
	public class CharGraphicComponent : IGameComponent
	{
		private readonly char _character;
		private readonly IConsoleWriter _consoleWriter;

		public CharGraphicComponent(char character, IConsoleWriter consoleWriter)
		{
			_character = character;
			_consoleWriter = consoleWriter;
		}
		public object Clone()
		{
			return new CharGraphicComponent(_character, _consoleWriter);
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
			_consoleWriter.PrintChars(gameObject.Boundary.LeftX, gameObject.Boundary.RightX,
				gameObject.Boundary.BottomY, gameObject.Boundary.TopY, _character, 1);
		}
	}
}

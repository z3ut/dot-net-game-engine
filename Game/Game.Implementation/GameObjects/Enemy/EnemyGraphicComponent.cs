using Game.Engine.Components;
using Game.Engine.Core;
using Game.Implementation.GameObjects.Enemy.Events;
using Game.Implementation.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.GameObjects.Enemy
{
	public class EnemyGraphicComponent : IGameComponent
	{
		private readonly IConsoleWriter _consoleWriter;
		private readonly char _displayCharNormalState;
		private readonly char _displayCharDestructionState;
		private char _currentChar;

		public EnemyGraphicComponent(IConsoleWriter consoleWriter,
			char displayCharNormalState, char displayCharDestructionState)
		{
			_consoleWriter = consoleWriter;
			_displayCharNormalState = displayCharNormalState;
			_displayCharDestructionState = displayCharDestructionState;

			_currentChar = _displayCharNormalState;
		}

		public object Clone()
		{
			return new EnemyGraphicComponent(_consoleWriter,
				_displayCharNormalState, _displayCharDestructionState);
		}

		public void Destroy(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public void HandleEvent(IGameObject gameObject, IGameWorld gameWorld, object gameEvent)
		{
			if (gameEvent is EnemyDestructionStart)
			{
				_currentChar = _displayCharDestructionState;
			}
		}

		public void Init(IGameObject gameObject, IGameWorld gameWorld)
		{

		}

		public void Update(IGameObject gameObject, IGameWorld gameWorld)
		{
			_consoleWriter.PrintChars(gameObject.Boundary.LeftX, gameObject.Boundary.RightX,
				gameObject.Boundary.BottomY, gameObject.Boundary.TopY, _currentChar, 1);
		}
	}
}

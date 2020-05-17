using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Implementation.Graphics
{
	public class ConsoleWriter : IConsoleWriter
	{
		private readonly int _charWidth;
		private readonly int _charHeight;
		private readonly int _coordsForChar;

		private float _cameraCenterX;
		private float _cameraCenterY;
		private float _cameraNextRenderCenterX;
		private float _cameraNextRenderCenterY;
		private IDictionary<Tuple<int, int>, int> _displayIndexes;

		public int Width { get { return _charWidth; } }
		public int Height { get { return _charHeight; } }

		public ConsoleWriter(int charWidth, int charHeight, int coordsForChar,
			float cameraCenterX, float cameraCenterY)
		{
			_charWidth = charWidth;
			_charHeight = charHeight;
			_cameraCenterX = cameraCenterX;
			_cameraCenterY = cameraCenterY;
			_cameraNextRenderCenterX = _cameraCenterX;
			_cameraNextRenderCenterY = _cameraCenterY;

			if (coordsForChar == 0)
			{
				throw new ArgumentException("Coords for char can't be zero");
			}
			_coordsForChar = coordsForChar;

			Console.SetWindowSize(charWidth, charHeight);
			Console.SetBufferSize(charWidth, charHeight);
			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
		}

		public void BeginRender()
		{
			Console.Clear();
			_displayIndexes = new Dictionary<Tuple<int, int>, int>();
			_cameraCenterX = _cameraNextRenderCenterX;
			_cameraCenterY = _cameraNextRenderCenterY;
		}

		public void EndRender()
		{
			
		}

		public void PrintChar(float x, float y, char character, int zIndex)
		{
			var left = GetHorizontalPosition(x);
			var top = GetVerticalPosition(y);

			PrintCharInPosition(left, top, character, zIndex);
		}

		public void PrintChars(float leftX, float rightX, float bottomY,
			float topY, char character, int zIndex)
		{
			var leftPositionX = GetHorizontalPosition(leftX);
			var topPositionY = GetVerticalPosition(topY);

			var widthDiff = rightX - leftX;
			var heightDiff = topY - bottomY;

			var widthBlocks = Math.Max((int)Math.Round(widthDiff / _coordsForChar), 1);
			var heightBlocks = Math.Max((int)Math.Round(heightDiff / _coordsForChar), 1);

			var rightPositionX = leftPositionX + widthBlocks;
			var bottomPositionY = topPositionY + heightBlocks;

			PrintCharsInPosition(leftPositionX, rightPositionX,
				bottomPositionY, topPositionY, character, zIndex);
		}

		public void PrintText(float x, float y, string text, int zIndex)
		{
			var positionX = GetHorizontalPosition(x);
			var positionY = GetVerticalPosition(y);

			PrintTextInPosition(positionX, positionY, text, zIndex);
		}

		public void PrintCharInPosition(int positionX, int positionY, char character, int zIndex)
		{
			var positionKey = Tuple.Create(positionX, positionY);

			if (_displayIndexes.TryGetValue(positionKey, out int savedZIndex) && savedZIndex >= zIndex)
			{
				return;
			}

			if (!IsLeftInConsoleBorder(positionX) || !IsTopInConsoleBorder(positionY))
			{
				return;
			}

			_displayIndexes[positionKey] = zIndex;
			Console.SetCursorPosition(positionX, positionY);
			Console.Write(character);
		}

		public void PrintCharsInPosition(int leftPositionX, int rightPositionX,
			int bottomPositionY, int topPositionY, char character, int zIndex)
		{
			for (var x = leftPositionX; x < rightPositionX; x++)
			{
				for (var y = topPositionY; y < bottomPositionY; y++)
				{
					PrintCharInPosition(x, y, character, zIndex);
				}
			}
		}
		public void PrintTextInPosition(int positionX, int positionY, string text, int zIndex)
		{
			for (var i = 0; i < text.Length; i++)
			{
				PrintCharInPosition(positionX + i, positionY, text[i], zIndex);
			}
		}

		public void PrintTextInPositionCenter(int positionX, int positionY, string text, int zIndex)
		{
			var positionXStart = positionX - text.Length / 2;
			PrintTextInPosition(positionXStart, positionY, text, zIndex);
		}

		public void PrintTextInPositionCenterInBox(int positionCenterX, int positionY,
			string text, char boxChar, int zIndex)
		{
			PrintCharsInPosition(positionCenterX - text.Length / 2 + 3,
				positionCenterX + text.Length / 2 + 3,
				positionY + 2, positionY - 2, boxChar, zIndex - 1);
			PrintTextInPositionCenter(positionCenterX, positionY, text, zIndex);
		}

		public void SetCameraCenter(float x, float y)
		{
			_cameraNextRenderCenterX = x;
			_cameraNextRenderCenterY = y;
		}	

		private int GetHorizontalPosition(float x)
		{
			return (int)Math.Round((x - _cameraCenterX) / _coordsForChar) + _charWidth / 2;
		}

		private int GetVerticalPosition(float y)
		{
			return (int)Math.Round(-(y - _cameraCenterY) / _coordsForChar) + _charHeight / 2;
		}

		private bool IsLeftInConsoleBorder(int left)
		{
			return 0 <= left && left < _charWidth;
		}

		private bool IsTopInConsoleBorder(int top)
		{
			return 0 <= top && top < _charHeight;
		}
	}
}

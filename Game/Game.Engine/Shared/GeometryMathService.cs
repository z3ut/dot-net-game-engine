using Game.Engine.Core;
using Game.Engine.Shared.Intersections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Shared
{
	public class GeometryMathService : IGeometryMathService
	{
		private readonly ILineIntersectionResolver _lineIntersectionResolver;

		public GeometryMathService(ILineIntersectionResolver lineIntersectionResolver)
		{
			_lineIntersectionResolver = lineIntersectionResolver;
		}

		public float Azimuth(float x1, float y1, float x2, float y2)
		{
			var rad = (float)Math.Atan2((x2 - x1), (y2 - y1));
			return RadiansToDegrees(rad);
		}

		public int DegreeToDirection(float degree)
		{
			return (int)Math.Floor(((degree + 45) % 360) / 90);
		}

		public int DegreeToQuarter(float degree)
		{
			return (int)Math.Floor((degree % 360) / 90);
		}

		public float Distance(float x1, float y1, float x2, float y2)
		{
			return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
		}

		public bool IsIntersects(Boundary b1, Boundary b2)
		{
			return b1.LeftX <= b2.RightX &&
				b1.RightX >= b2.LeftX &&
				b1.TopY >= b2.BottomY &&
				b1.BottomY <= b2.TopY;
		}

		public bool IsIntersects(Boundary b, float lineX1, float lineY1, float lineX2, float lineY2)
		{
			return _lineIntersectionResolver.IsIntersect(b.LeftX, b.BottomY, b.RightX, b.TopY,
				lineX1, lineY1, lineX2, lineY2);
		}

		public (float x, float y) PointOnBoundary(IGameObject gameObject, float azimuth)
		{
			float pointX = gameObject.X;
			float pointY = gameObject.Y;

			switch (DegreeToDirection(azimuth))
			{
				case 0:
					pointY += gameObject.Height / 2 + 1;
					break;
				case 1:
					pointX += gameObject.Width / 2 + 1;
					break;
				case 2:
					pointY -= gameObject.Height / 2 + 1;
					break;
				case 3:
					pointX -= gameObject.Width / 2 + 1;
					break;
			}

			return (pointX, pointY);
		}

		public float RadiansToDegrees(float rad)
		{
			return ((float)(rad * 180 / Math.PI) + 360) % 360;
		}
	}
}

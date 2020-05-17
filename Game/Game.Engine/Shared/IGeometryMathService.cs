using Game.Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine.Shared
{
	public interface IGeometryMathService
	{
		float Distance(float x1, float y1, float x2, float y2);
		float RadiansToDegrees(float rad);
		float Azimuth(float x1, float y1, float x2, float y2);
		bool IsIntersects(Boundary b1, Boundary b2);
		bool IsIntersects(Boundary b, float lineX1, float lineY1, float lineX2, float lineY2);
		(float x, float y) PointOnBoundary(IGameObject gameObject, float azimuth);
		int DegreeToQuarter(float degree);
		int DegreeToDirection(float degree);
	}
}

using UnityEngine;
using System.Collections;

public class MathfPlus 
{
	public static Vector3 GetClosestAngle(Vector3 oldRotation, Vector3 newRotation)
	{
		float x = GetClosestAngle(oldRotation.x, newRotation.x);
		float y = GetClosestAngle(oldRotation.y, newRotation.y);
		float z = GetClosestAngle(oldRotation.z, newRotation.z);
		
		return new Vector3(x, y, z);
	}
	
	public static float GetClosestAngle(float oldRotation, float newRotation)
	{
		float newValue = newRotation;
		if (oldRotation - newRotation > 180.0f)
		{
			newValue += 360.0f;
		}
		else if (oldRotation - newRotation < -180.0f)
		{
			newValue -= 360.0f;
		}
		
		return newValue;
	}
	
	public static float GetUniversalAngle(float angle)
	{
		// TODO: It can be better without a while.
		while (angle < 0.0f || angle >= 360.0f)
		{
			if (angle < 0.0f)
			{
				angle += 360.0f;
			}
			else if (angle >= 360.0f)
			{
				angle -= 360.0f;
			}
		}
		
		return angle;
	}
	
	public static float QuartEaseOut(float t, float b, float c, float d)
    {
        return -c * ( ( t = t / d - 1 ) * t * t * t - 1 ) + b;
    }
}

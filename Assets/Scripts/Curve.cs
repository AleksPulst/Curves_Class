using UnityEngine;

public class Curve : MonoBehaviour
{
	public Vector3[] points;
	public Vector3[] tangents;

	public void Reset()
	{
		points = new Vector3[]
		{
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
            new Vector3(3f, 0f, 0f),
        };
	}

    public void OnValidate()
    {
		// Match the amount of tangents when a new point is added. This code prevents adding a tangent without a point.
		if (points.Length != tangents.Length)
		{
			bool pointAdded = points.Length > tangents.Length;
            Vector3[] temp = tangents;
			tangents = new Vector3[points.Length];

			// If a new point is added the add it to the mirrored tangets position.
			for (int i = 0; i < points.Length; i++)
			{
				if (i < temp.Length)
				{
					// Copy the existing tangent
					tangents[i] = temp[i];        
                }
				else
				{
					if (pointAdded && i > 0)
					{
						// Adding a new point and setting the point to the mirrored tangetn
						points[i] = (points[i-1] - tangents[i-1]) + points[i-1];
						tangents[i] = points[i] + (points[i-1] - points[i]).normalized;
                        Debug.Log("tangent");
                    }
					else
					{
						// If not preivous point is found place the tangent just a bit forward of the point.
						tangents[i] = points[i] + Vector3.forward;
						Debug.Log("first");
					}

				}
			}
		}

    }

    public Vector3 GetPoint(float t)
	{
		if (points.Length < 2)
		{
			return transform.position;
		}

		if (t >= 1)
		{
			return transform.TransformPoint(points[points.Length - 1]);
		}

		int startIndex = Mathf.FloorToInt(t*(points.Length-1));
		t = t * (points.Length-1) % 1;

		if (startIndex == 0)
		{
			return transform.TransformPoint(MathHelp.GetCurvePoint(points[startIndex], tangents[startIndex], tangents[startIndex+1], points[startIndex+1] , t));
		}
		else
		{
			Vector3 mirror_tangent = (points[startIndex] - tangents[startIndex]) + points[startIndex];
            return transform.TransformPoint(MathHelp.GetCurvePoint(points[startIndex], mirror_tangent, tangents[startIndex + 1], points[startIndex + 1], t));
        }

	}
}

using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Curve))]
public class CurveInspector : Editor
{
	private Curve m_Curve;
	private Transform m_tHandleTransform;
	private Quaternion m_qHandleRotation;

	[SerializeField] private const int m_iDebugDrawLineSteps = 30;
	
	private void OnSceneGUI()
	{
		m_Curve = target as Curve;
		m_tHandleTransform = m_Curve.transform;
		m_qHandleRotation = Tools.pivotRotation == PivotRotation.Local ? m_tHandleTransform.rotation : Quaternion.identity;


		for (int i = 0; i < m_Curve.points.Length; i++)
		{
            Handles.color = Color.blue;
            Vector3 p0 = ShowPoint(i);
            Vector3 t0 = ShowTangent(i);
			Vector3 t1 = p0 - t0 + p0;

			Handles.DrawLine(t0, t1);

            Handles.color = Color.yellow;
            if (i < m_Curve.points.Length - 1)
			{
				Vector3 p1 = m_Curve.transform.TransformPoint(m_Curve.points[i + 1]);
				Handles.DrawLine(p0, p1);
			}
		}
		
		


        Vector3 lineStart = m_Curve.GetPoint(0f);
        Handles.color = Color.white;
		int resolution = m_iDebugDrawLineSteps * m_Curve.points.Length;

        for (int j = 1; j <= resolution; j++)
        {
            Vector3 lineEnd = m_Curve.GetPoint(j / (float)resolution);
            Handles.DrawLine(lineStart, lineEnd);
            lineStart = lineEnd;
        }

    }


    Vector3 ShowPoint(int index)
	{
		Vector3 point = m_tHandleTransform.TransformPoint(m_Curve.points[index]);	
		
		EditorGUI.BeginChangeCheck();
		point = Handles.DoPositionHandle(point, m_qHandleRotation);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(m_Curve, "Move Point");
			EditorUtility.SetDirty(m_Curve);
			m_Curve.points[index] = m_tHandleTransform.InverseTransformPoint(point);
		}
		
		
		return point;
	}
    Vector3 ShowTangent(int index)
    {
        Vector3 point = m_tHandleTransform.TransformPoint(m_Curve.tangents[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, m_qHandleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_Curve, "Move Point");
            EditorUtility.SetDirty(m_Curve);
            m_Curve.tangents[index] = m_tHandleTransform.InverseTransformPoint(point);
        }


        return point;
    }

}

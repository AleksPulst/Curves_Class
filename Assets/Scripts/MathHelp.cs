using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public static class MathHelp
{
    public static Vector3 GetCurvePoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);
        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);
        Vector3 p = Vector3.Lerp(d, e, t);
        return p;
    }
}

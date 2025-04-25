using UnityEngine;

public class MrRectangle : MonoBehaviour
{
    [SerializeField]
    private Curve m_curve;

    
    // Update is called once per frame
    void Update()
    {
        transform.position = m_curve.GetPoint(Mathf.PingPong(Time.time,1.0f));
    }
}

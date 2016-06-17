using UnityEngine;
using System.Collections;

public class ArcRendererTest : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;

    float time = 5;
    Vector3 gravity;

    void Start()
    {
        gravity = Physics.gravity;
    }

    void Update()
    {
        

        
    }

    Vector3 CalculateTrajectory(Vector3 from, Vector3 to, float time)
    {
        Vector3 graivity = Physics.gravity;
        Vector3 velocityNeeded = Vector3.zero;
        //Vector3 dir = to - from;

        //dir = Quaternion.LookRotation(to-from, Vector3.up) * dir;

        velocityNeeded.z = (to.z - from.z) / time;
        velocityNeeded.y = (to.y + 0.5f * gravity.y * time * time - from.y) / time;


        return velocityNeeded;
    }
}

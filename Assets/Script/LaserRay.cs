using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour
{
    LineRenderer line_renderer;
    Transform child_object;
    // Start is called before the first frame update
    void Start()
    {
        line_renderer=GetComponent<LineRenderer>();
        child_object=transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        var poses = PhysicsUtil.RefrectionLinePoses(child_object.position,child_object.forward, 50f,0).ToArray();
        line_renderer.positionCount = poses.Length;
        line_renderer.SetPositions(poses);
    }
}

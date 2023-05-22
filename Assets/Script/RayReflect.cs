using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtil
{
    public static List<Vector3> RefrectionLinePoses(Vector3 position, Vector3 direction, float length, LayerMask layerMask)
    {
        var points = new List<Vector3>() { position };
        while (Physics.Raycast(position, direction, out var hit, length, ~layerMask))
        {
            
            position = hit.point;
            points.Add(position);
            if(hit.transform.tag!="Mirror")
            {
                length=0;
                direction =Vector3.zero;
            }
            length -= hit.distance;
            direction = Vector3.Reflect(direction, hit.normal);
        }
        points.Add(position + direction * length);
        return points;
    }
    public static List<Vector3> RefrectionLinePoses(Vector3 position, float radius, Vector3 direction, float length, LayerMask layerMask)
    {
        var points = new List<Vector3>() { position };
        while (Physics.SphereCast(position, radius, direction, out var hit, length, ~layerMask)||hit.transform.tag=="Mirror")
        {
            if(hit.transform.tag!="Mirror")
                break;
            position = hit.point + hit.normal * radius;
            points.Add(position);
            length -= hit.distance;
            direction = Vector3.Reflect(direction, hit.normal);
        }
        points.Add(position + direction * length);
        return points;
    }
}
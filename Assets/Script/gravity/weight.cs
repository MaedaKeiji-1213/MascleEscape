using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weight : MonoBehaviour
{
    public Vector3 wei;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionStay(Collision collision)
    {
        wei = collision.impulse;
        Debug.Log("wei:" + wei.y);
    }
}

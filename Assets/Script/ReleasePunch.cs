using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasePunch : MonoBehaviour
{
    Animator animator;
    [System.NonSerialized]public bool is_release=false;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetRelease()
    {
        is_release=true;
    }
}

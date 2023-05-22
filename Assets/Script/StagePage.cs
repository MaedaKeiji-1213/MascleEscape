using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePage : MonoBehaviour
{
    public GameObject right_page=null;
    public GameObject left_page=null;
    public GameObject up_page=null;
    public GameObject under_page=null;
    // Start is called before the first frame update
    void Start()
    {
        right_page=right_page==null?gameObject:right_page;
        left_page=left_page==null?gameObject:left_page;
        up_page=up_page==null?gameObject:up_page;
        under_page=under_page==null?gameObject:under_page;
    }

    // Update is called once per frame
    void Update()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    GameObject current_page_object;
    StagePage stage_page;
    [SerializeField] GameObject right_button;
    [SerializeField] GameObject left_button;
    [SerializeField] GameObject up_button;
    [SerializeField] GameObject under_button;
    [SerializeField] float scroll_speed;
    // Start is called before the first frame update
    void Start()
    {
        current_page_object=transform.GetChild(transform.childCount-1).gameObject;
        stage_page=current_page_object.GetComponent<StagePage>();
        right_button.SetActive(stage_page.right_page!=current_page_object);
        left_button .SetActive(stage_page.left_page!=current_page_object);
        up_button   .SetActive(stage_page.up_page!=current_page_object);
        under_button.SetActive(stage_page.under_page!=current_page_object);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center_position=new Vector3(Camera.main.pixelWidth/2,Camera.main.pixelHeight/2,0);
        transform.Translate((center_position-current_page_object.transform.position)*scroll_speed);
    }

    public void RightButton()
    {
        current_page_object=stage_page.right_page;
        stage_page=current_page_object.GetComponent<StagePage>();
        right_button.SetActive(stage_page.right_page!=current_page_object);
        left_button .SetActive(stage_page.left_page!=current_page_object);
        up_button   .SetActive(stage_page.up_page!=current_page_object);
        under_button.SetActive(stage_page.under_page!=current_page_object);
    }
    public void LeftButton()
    {
        current_page_object=stage_page.left_page;
        stage_page=current_page_object.GetComponent<StagePage>();
        right_button.SetActive(stage_page.right_page!=current_page_object);
        left_button .SetActive(stage_page.left_page!=current_page_object);
        up_button   .SetActive(stage_page.up_page!=current_page_object);
        under_button.SetActive(stage_page.under_page!=current_page_object);
    }
    public void UpButton()
    {
        current_page_object=stage_page.up_page;
        stage_page=current_page_object.GetComponent<StagePage>();
        right_button.SetActive(stage_page.right_page!=current_page_object);
        left_button .SetActive(stage_page.left_page!=current_page_object);
        up_button   .SetActive(stage_page.up_page!=current_page_object);
        under_button.SetActive(stage_page.under_page!=current_page_object);
    }
    public void UnderButton()
    {
        current_page_object=stage_page.under_page;
        stage_page=current_page_object.GetComponent<StagePage>();
        right_button.SetActive(stage_page.right_page!=current_page_object);
        left_button .SetActive(stage_page.left_page!=current_page_object);
        up_button   .SetActive(stage_page.up_page!=current_page_object);
        under_button.SetActive(stage_page.under_page!=current_page_object);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInteraction : MonoBehaviour
{
    private GameObject[] ClickObjects;
    private GameObject[] AnimTriggerObjects;

     GameObject Target=null;
    public GameObject TriggerTarget { get; private set; }
    public GameObject ClickTarget { get; private set; }

    private void Start()
    {
        TriggerTarget = null;
        ClickTarget = null;
        Target = null;

        ClickObjects = GameObject.FindGameObjectsWithTag("ClickObject");
        AnimTriggerObjects = GameObject.FindGameObjectsWithTag("AnimTriggerObject");
    }



    private void Update()
    {
        Click();
    }

    protected  void RaycastTarget()
    {
        Target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null) { Target = hit.collider.gameObject; }
        
    }

    protected virtual void Click()
    {
        RaycastTarget();

        if (Input.GetMouseButtonDown(0))
        {
            if (Target != null)
            {
                foreach (GameObject clickObj in ClickObjects)
                {
                     if (Target.tag == "AnimTriggerObject")
                    {
                        TriggerTarget = Target;
                         TriggerTarget.GetComponent<BoxCollider2D>().enabled = false;

                        GameObject.FindWithTag("Chapter").GetComponent<ChapterManager>().Play_StoryAnimation();
                    }
                    else if (Target.tag == "ClickObject")
                    {
                        ClickTarget = Target;
                        GameObject.FindWithTag("Chapter").GetComponent<ChapterManager>().ClickObject();
                    }
                }
            }
        }
    }

    
}

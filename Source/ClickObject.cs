using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : ObjectsWithIndex
{
    public bool bIsTrigger { get; private set; }

    void Start()
    {
        bIsTrigger = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AnimTriggerObject")
        {
            if (GameManager.Instance.GetChapter3())
            {
                collision.enabled = false;
                GameManager.Instance.GetChapter3().Cubic_Interaction.Trigger(collision.gameObject);
                
            }
        }
    }



}

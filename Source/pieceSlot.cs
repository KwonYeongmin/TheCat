using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pieceSlot : MonoBehaviour,IDropHandler
{
  
    public void OnDrop(PointerEventData eventdata) 
    {
       // Debug.Log("OnDrop");
        
        if (eventdata.pointerDrag != null) 
            {
            eventdata.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
            } 
    }


  
}

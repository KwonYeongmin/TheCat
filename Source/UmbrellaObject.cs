using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaObject : MonoBehaviour
{

    //UI Stroke
    private SpriteRenderer UmbrellaStroke;
    private Color[] StrokeColor;

    private float startPosX;
    private float startPosY;
    public bool bisBeginHeld = false;
    public RainInteraction InteractionController;

    private void Awake()
    {
        bisBeginHeld = false;
      
        // Stroke
        StrokeColor = new Color[2];
        StrokeColor[0] = new Color(1f,1f,1f,1f);
        StrokeColor[1] = new Color(1f,1f,1f,0f);

        UmbrellaStroke = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (bisBeginHeld) 
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x,mousePos.y,0);
        }
    }


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

           startPosX = mousePos.x - this.transform.localPosition.x;
           startPosY = mousePos.y - this.transform.localPosition.y;

            bisBeginHeld = true;


            // Feedback
            UmbrellaStroke.color = StrokeColor[0];
            SoundManager.Instance.PlayUIAudio(SoundList.Sound_Drag);
        } 

       
    }

    private void OnMouseUp()
    {
        bisBeginHeld = false;

        // Feedback
        //SoundManager.Instance.PlayUIAudio(SoundList.Sound_Drop);
        UmbrellaStroke.color = StrokeColor[1];
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "AnimTriggerObject" && bisBeginHeld)
        {
            // Feedback
            //SoundManager.Instance.PlayUIAudio(SoundList.Sound_Touch);
            UmbrellaStroke.color = StrokeColor[1];

            if(InteractionController) InteractionController.EndInteraction();
            bisBeginHeld = false;
        }
    }

}

    

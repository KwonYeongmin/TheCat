using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubic : MonoBehaviour
{
    private GameObject Cubic_Stroke;


    float startPosX;
    float startPosY;
    bool bisBeginHeld = false;


    void Start()
    {
        Reset();
    }
    private void Update()
    {
        if (bisBeginHeld)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }
    private void Reset()
    {
        Cubic_Stroke = this.transform.GetChild(0).gameObject;
        Cubic_Stroke.SetActive(false);

        bisBeginHeld = false;
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

            //feed
            Cubic_Stroke.SetActive(true);
            SoundManager.Instance.PlayUIAudio(SoundList.Sound_Drag);
        }
    }

    private void OnMouseUp()
    {
        bisBeginHeld = false;

        // Feedback
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Drop);
        Cubic_Stroke.SetActive(false);
    }
}

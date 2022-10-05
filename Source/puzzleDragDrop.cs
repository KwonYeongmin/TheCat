
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
//using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;

public class puzzleDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler{

    [SerializeField]  Canvas canvas;
    [SerializeField] touchUI_ch1 touchUI;
    //  [SerializeField] levelManager_Ch01 levelM;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector3 initialScale;
    private Vector3 activelScale;

    public int stroke_index;
    public GameObject puzzle_slot;
  

    private void Start()
    {
        rectTransform.sizeDelta = initialScale;

        canvas = FindObjectOfType<Canvas>();
        touchUI = FindObjectOfType<touchUI_ch1>();
    }

    

    private void Awake() {
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        initialScale = rectTransform.sizeDelta*0.8f;

        activelScale = rectTransform.sizeDelta;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
       

        rectTransform.sizeDelta = activelScale;

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        // levelM.UI_sound(4);

        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Drag);
      
    }

    public void OnDrag(PointerEventData eventData) {
       

       rectTransform.sizeDelta = activelScale;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        touchUI.piece_stroke(stroke_index,1f);
      
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.sizeDelta = activelScale;
        touchUI.piece_stroke(stroke_index, 0f);
        touchUI.Dial_UI(2, false);
        
        //
        if(this.gameObject.GetComponent<RectTransform>().localPosition == puzzle_slot.GetComponent<RectTransform>().localPosition) 
        {
            
            GameObject.FindWithTag("Interaction").GetComponent<puzzleController>().puzzleCount++;
            puzzle_slot.GetComponent<pieceSlot>().enabled = false;
            puzzle_slot.SetActive(true);
            this.gameObject.GetComponent<puzzleDragDrop>().enabled = false;
        }

    }

    public void OnPointerDown(PointerEventData eventData) {

        rectTransform.sizeDelta = activelScale;
        touchUI.piece_stroke(stroke_index, 1f);

        if (this.gameObject.GetComponent<RectTransform>().localPosition == puzzle_slot.GetComponent<RectTransform>().localPosition)
        {
            SoundManager.Instance.PlayUIAudio(SoundList.Sound_Click);

        }


    }

    public void OnDrop(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIList : MonoBehaviour
{
    public GameObject UI_controlInfo;
    public GameObject UI_speechBubble;
    public GameObject UI_dragNDrop_chapter3;
    public GameObject UI_click;
    public GameObject UI_footstep;

    public static GameObject UI_ControlInfo { get; private set; }
    public static GameObject UI_SpeechBubble { get; private set; }
    public static GameObject UI_DragNDrop_chapter3 { get; private set; }
    public static GameObject UI_Click { get; private set; }
    public static GameObject UI_Footstep { get; private set; }

    protected void Awake()
    {
        UI_SpeechBubble = UI_speechBubble;
        UI_DragNDrop_chapter3 = UI_dragNDrop_chapter3;
        UI_Click = UI_click;
        UI_Footstep = UI_footstep;
        UI_ControlInfo = UI_controlInfo;
    }


}

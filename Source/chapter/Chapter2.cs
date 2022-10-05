using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2 : ChapterManager
{
    [Header("Data Reader")]
    private ShowDialData Data_Show;

    [Header("Animator")]
    public Animator[] animator;

    [Header("Chapter2 GameObject")]
    private EventBox[] EventBoxes;
    private Sunhwa Sunhwa;
    public GameObject[] ObjectAnim;
    public GameObject StoneBridge;
    public GameObject Umbrella;
    public ClickInteraction clickInteraction { get; private set; }
    public GameObject[] SceneAnimation;
    public GameObject Background;
    public Sprite Sunhwa_Tail_Sprite;
    public Transform RiverPos;
    [Header("Dialogue Length")]
    public int DialNum;
    public int[,] Dial_Array { get; private set; }
    public int[] EndDial;
    private int EndIndex = 0;


    [Header("Interation")]
    public RainInteraction Rain_Interaction;
    public BridgeInteraction Bridge_Interaction;
    public GameObject Bridge_Interaction_Btn;

    [Header("Fadeinout")]
    int FadeInout_Count = 0;
    int AnimIndex = 0;
    public GameObject FadeinoutPanel;


    private void Awake()
    {
        Data_Show = this.GetComponent<ShowDialData>();
        clickInteraction = this.GetComponent<ClickInteraction>();
    }

    [Header("Footstep Transform")]
    public Transform[] FootstepPosition;

    private void Start()
    {
        PlayBGM(SoundList.Chapter2_Out);
        SetDial();

        EventBoxes = new EventBox[3];
        for (int i = 0; i < EventBoxes.Length; i++)
            EventBoxes[i] = GameObject.FindWithTag("EventBox").GetComponent<EventBox>();

        Sunhwa = GameObject.FindWithTag("Player").GetComponent<Sunhwa>();

        Reset();


        UIManager.Instance.CreateAtCanvas(UIList.UI_ControlInfo, new Vector2(Screen.width / 2, Screen.height / 2)); //0
        UIManager.Instance.CreateAtLocation(UIList.UI_Footstep, FootstepPosition[0].position - new Vector3(0, 2f, 0)); //1

        UIManager.Instance.CreateAtLocation_Parent(UIList.UI_SpeechBubble, animator[1].transform.position + new Vector3(-2f, 2f, 0), animator[1].transform); //1

    }

    private void Reset()
    {
        for (int i = 0; i < SceneAnimation.Length; i++)
            SceneAnimation[i].SetActive(false);


        for (int i = 0; i < 4; i++) animator[0].gameObject.SetActive(true);
        Umbrella.SetActive(true);

    }

    // ================================================ Animation ================================================

    public void Play_SpinxBoxMove()
    {
        AnimationSetBool(animator[1], "SPBoxMove", true);
        SoundManager.Instance.PlayMergedBGM(SoundList.Sound_BoxMove);
    }

    private void Play_ArrowBtn()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Arrowbutton);
    }


    // ================================================ AnimationEvent ================================================
    public void Play_ClickUI()
    {
        Debug.Log("상자 클릭 UI 아이콘");
        UIManager.Instance.CreateAtLocation(UIList.UI_Click,animator[1].transform.position-new Vector3(0,1f,0));
    }

    public void Toggle_SceneAnimation(int index, bool value)
    {
        SceneAnimation[index].SetActive(value);
    }

    public override void Active_FadePanel()
    {
        FadeinoutPanel.SetActive(true);
    }

    public void Play_ClockInteraction()
    {
        Rain_Interaction.StartInteraction();
    }

    public void Play_SpinxSound()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Spinx);
    }

    public void Play_SunhwaTailSound()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_tail);
        AnimationSetBool(animator[0], "isSHTail", true);
        Sunhwa.GetComponent<SpriteRenderer>().sprite = Sunhwa_Tail_Sprite;
    }

    // ================================================ Function ================================================

    public override void TriggerEventBox()
    {
        if (Sunhwa.bIsTrigger)
        {
            int index = Sunhwa.eventBox.Index;

            switch (index)
            {
                case 0:
                    {
                        StartDL();
                        UIManager.Instance.Destroy_UI(0);
                    } break;
                case 1:
                    {
                        StartDL();
                        UIManager.Instance.Destroy_UI(3);
                        Play_SpinxBoxMove();
                    } break;
                case 2: {
                        Sunhwa.SHNotMove();
                        UIManager.Instance.Destroy_UI(4);
                    }
                    break;
            }
            Sunhwa.eventBox.DestroyObject();

            Sunhwa.bIsTrigger = false;
        }
    }


    public override void Play_StoryAnimation()
    { 
        clickInteraction.TriggerTarget.GetComponent<BoxCollider2D>().enabled = false;
        Active_FadePanel();
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Touch);
        
    }

    public override void ClickObject()
    {
        GameObject target = clickInteraction.ClickTarget;
        int index = 0;
        if (target.GetComponent<ClickObject>())
            index = target.GetComponent<ClickObject>().Index;

        if (!Bridge_Interaction.bIsInteractionStart)
        {
            switch (index)
            {
                case 0:
                    {
                        StartDL();
                        target.GetComponent<Collider2D>().enabled = false;
                        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Touch);
                        UIManager.Instance.Destroy_UI(6);

                    }
                    break;
                case 1:  Start_RainInteraction(); break;
            }
        }
        else
            Bridge_Interaction.TouchEvent();
    } 



  


    public override void Play_Fadeinout()
    {
        switch (FadeInout_Count)
        { 
            case 0: { Toggle_SceneAnimation(0, true); UIManager.Instance.Destroy_UI(2);
                    UIManager.Instance.Destroy_UI(5);

                }
                break;
            case 1: { Start_BridgeInteraction(); } break;
            case 2: {
                    Toggle_SceneAnimation(1, true);
                    Background.SetActive(true);
                    Toggle_SceneAnimation(2, false);
                    Bridge_Interaction.Reset();
                } break;
            case 3: { StartDL(); SoundManager.Instance.StopMergedBGM(); } break;
            case 4: { StartDL(); } break;
            case 5:
                {
                    Start_BridgeInteraction();
                }
                break;
            case 6:
                {
                    UIManager.Instance.ClearObjects();
                    ChangeScene(3);
                } break;
        }
        FadeInout_Count++;
    }

    public override void End_SceneAnimation()
    {
        Debug.Log(FadeInout_Count);
        switch (FadeInout_Count)
        {
            case 1: { Toggle_SceneAnimation(0,false); StartDL(); } break;
            case 3:
                {
                    Toggle_SceneAnimation(1, false);
                    for( int i = 1; i<= 3;i++) animator[i].gameObject.SetActive(false);
                    Umbrella.SetActive(false);
                    Active_FadePanel();
                } break;
            case 4:
                {

                    Toggle_SceneAnimation(3, false);                    
                    Active_FadePanel();
                } break;
        }
       
    }


    private void Start_RainInteraction()
    {
        Rain_Interaction.StartInteraction();
        animator[2].GetComponent<BoxCollider2D>().enabled = true;
        animator[2].GetComponent<BoxCollider2D>().size *= 0.5f;
    }

    public void End_RainInteraction()
    {
        animator[4].gameObject.SetActive(false);
        AnimationSetBool(animator[2], "isSPWalk", true);
        AnimationSetBool(animator[3], "isSPOut", true);
        

    }


    private void Start_BridgeInteraction()
    {
        Toggle_SceneAnimation(2, true);
        Background.SetActive(false);
        Bridge_Interaction.StartInteraction();
    }

    public void End_BridgeInteraction()
    {
        DownArrowBtn_Audio();
        Bridge_Interaction_Btn.SetActive(false);
        Active_FadePanel();      
    }


    // ================================================ Dialogue ================================================
    public void SetDial()
    {
        EndIndex = 0;
        Dial_Array = new int[2, DialNum];

        Dial_Array[0, 0] = 0;

        int count = 0;

        for (int i = 0; i < EndDial.Length; i++)
        {
            Dial_Array[1, i] = EndDial[i];
            if (i + 1 > EndDial.Length - 1) break;
            Dial_Array[0, i + 1] = EndDial[i] + 1;
            count++;
        }
    }
    private bool bIsHaveObject = false;
    private int ObjectIndex = 0;

    private void SetObject()
    {
      //  Debug.Log("EndIndex: " + EndIndex);
      //  Debug.Log("DialIndex: " + Data_Show.Index);
        if (EndIndex == 5)
        {
            bIsHaveObject = true;
            ObjectIndex = 1;
        }
        else
            bIsHaveObject = false;
 
    }



    public override void StartDL()
    {
        base.StartDL();

        Data_Show.ToggleText(true);
        Data_Show.ShowText();
        Sunhwa.SHNotMove();

        SetObject();

        if (bIsHaveObject) ObjectAnim[ObjectIndex].SetActive(true);
        else ObjectAnim[ObjectIndex].SetActive(false);

        if(Data_Show.Index == 2)
            SoundManager.Instance.StopMergedBGM();

    }

    public override void DownDialogueBtn()
    {
        base.DownDialogueBtn();
        if (Dial_Array[1, EndIndex] <= Data_Show.Index)
        {
            EndIndex++;
            Data_Show.NextDialIndex();
            ObjectAnim[ObjectIndex].SetActive(false);
            EndDL();
        }
        else Data_Show.NextDialIndex();

        if (Data_Show.Index == 19)
        {
            ObjectAnim[ObjectIndex].SetActive(false);
            ObjectIndex = 2;
            ObjectAnim[ObjectIndex].SetActive(true);
            SoundManager.Instance.PlayMergedBGM(SoundList.Chapter2_Guitar);
        }

    }


    public override void EndDL()
    {

        Data_Show.ToggleText(false);
        Data_Show.InActiveAnim();
        switch (EndIndex)
        {
            case 1:
                {
                    Sunhwa.SHMove();
                    UIManager.Instance.Destroy_UI(1);
                    UIManager.Instance.CreateAtLocation(UIList.UI_Footstep, FootstepPosition[1].position - new Vector3(0, 2f, 0)); //1
                }  break;
            case 2:
                {
                    animator[2].gameObject.GetComponent<Collider2D>().enabled = true;
                    UIManager.Instance.CreateAtLocation(UIList.UI_Footstep, FootstepPosition[2].position - new Vector3(0, 2f, 0));
                    Sunhwa.SHMove();
                }
                break;
            case 3:
                {
                    UIManager.Instance.CreateAtLocation(UIList.UI_Click, RiverPos.position);
                    StoneBridge.GetComponent<Collider2D>().enabled = true;
                    SoundManager.Instance.PlayMergedBGM(SoundList.Sound_RiverSFX);
                }
                break;
            case 4:
                {
                    SoundManager.Instance.PlayMergedBGM(SoundList.Chapter2_Rain);
                    animator[4].gameObject.SetActive(true);
                    AnimationSetBool(animator[4], "isRaining", true);
                } break;
            case 5:
                {
                    Umbrella.GetComponent<Collider2D>().enabled = true;
                } break;
            case 6: { Active_FadePanel(); }
                break;
            case 7: { Toggle_SceneAnimation(3, true); } break;
            case 8: { Active_FadePanel(); } break;
        }
    }

    
}

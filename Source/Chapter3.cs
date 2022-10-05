using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter3 : ChapterManager
{
    [Header("Data Reader")]
    private ShowData[] Data_Show = new ShowData[2];

    [Header("Animator")]
    public Animator[] animator;

    [Header("Chapter3 GameObject")]
    public EventBox EventBoxe;
    private Sunhwa Sunhwa;

    [Header("Dialogue Length")]
    public int DialNum;
    public int[,] Dial_Array { get; private set; }
    public int[] EndDial;
    private int EndIndex = 0;

    [Header("Fadeinout")]
    public GameObject FadeinoutPanel;
    private int Fadeinout_Count = 0;

    [Header("Manager")]
    public IllustrateController illustrate_Controller;
    public CubicInteraction Cubic_Interaction;

    [Header("Animation")]
    public Animator[] RisingSunScene;
    public GameObject RainbowScene;
    public GameObject CubicParticle;
    public GameObject AnimScene;
    public GameObject CatStar;

    [Header("Footstep Transform")]
    public Transform[] FootstepPosition;

    

    private void Awake()
    {
        Data_Show[0] = this.GetComponent<ShowDialData>();
        Data_Show[1] = this.GetComponent<ShowDialData>();

      //  clickInteraction = this.GetComponent<ClickInteraction>();
    }

    private void Start()
    {
        PlayBGM(SoundList.Chapter3_Out);
        SetDial();

        Sunhwa = GameObject.FindWithTag("Player").GetComponent<Sunhwa>();

        Sunhwa.SHNotMove();

    }

    private void Reset()
    {
        Fadeinout_Count = 0;
        EventBoxe.GetComponent<Collider2D>().enabled = true;
    }


    // ================================================ Animation ================================================


    // ================================================ AnimationEvent ================================================

    public override void Active_FadePanel()
    {
        FadeinoutPanel.SetActive(true);
    }

    public void Play_RainbowScene()
    {
        RainbowScene.SetActive(true);
        AnimationSetBool(RainbowScene.GetComponent<Animator>(), "risingSun", true);
    }

    public void Play_CubicParticle()
    {
        CubicParticle.SetActive(true);
    }

    public void Move_CameraView()
    {
        AnimationSetBool(animator[0], "nero_bye", true);
        BackgroundSceneObject[1].GetComponent<Animator>().enabled = true;
        AnimationSetBool(BackgroundSceneObject[1].GetComponent<Animator>(), "BG_move", true);
    }

    public void Play_GlowCatStar()
    {
        Invoke("GlowCatStar", 1.5f);
    }

    public override void End_SceneAnimation()
    {
        AnimScene.SetActive(false);
        Active_FadePanel();
        
    }

    // ================================================ Function ================================================

    public override void TriggerEventBox()
    {
        if (Sunhwa.bIsTrigger)
        {
            StartDL();
            Sunhwa.eventBox.DestroyObject();
            UIManager.Instance.Destroy_UI(0);
            UIManager.Instance.Destroy_UI(1);
            UIManager.Instance.Destroy_UI(2);
        }

        Sunhwa.bIsTrigger = false;
    }



    public override void Play_Fadeinout()
    {
        if (Fadeinout_Count < 9)
        {
            if (Fadeinout_Count % 2 == 0)
            {
                End_Story();
                if (Fadeinout_Count == 8)
                    SoundManager.Instance.PlayBGM(SoundList.Chapter3_Out);
            }
            else Start_Story();
        }
        else if (Fadeinout_Count == 9) { StartDL(); }
        else if (Fadeinout_Count == 10) { ChangeScene(4);  }

        Fadeinout_Count++;
    }

   
   


    private void Start_Story()
    {
        illustrate_Controller.StartInteraction();
    }

    private void End_Story()
    {
      
        StartDL();
        illustrate_Controller.InactiveImages();

    }

    public GameObject[] BackgroundSceneObject;

    private void Start_CubicInteraction()
    {
        BackgroundSceneObject[0].GetComponent<cameraController>().enabled = false;
        BackgroundSceneObject[0].GetComponent<Transform>().position = new Vector3(8.53f, -0.16f, -10f);
        BackgroundSceneObject[1].GetComponent<Transform>().localScale = new Vector3(1.55f, 1.55f, 1f);

        SoundManager.Instance.PlayBGM(SoundList.Chapter3_In);
        Cubic_Interaction.StartInteraction();
    }

    public void End_CubicInteraction()  
    {
        for (int i = 0; i < RisingSunScene.Length; i++)
            AnimationSetBool(RisingSunScene[i], "risingSun", true);
    }


    private void GlowCatStar()
    {
        CatStar.gameObject.SetActive(true);
        Invoke("Play_StoryAnimation", 3f);
    }

    public override void Play_StoryAnimation()
    {
        AnimScene.SetActive(true);
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

    public override void StartDL()
    {
        base.StartDL();

        GetDialData().ToggleText(true);
        GetDialData().ShowText();
        Sunhwa.SHNotMove();
       // Sunhwa.transform.GetChild(0).GetComponent<Animator>().enabled = false;
    }

    public override void DownDialogueBtn()
    {
        base.DownDialogueBtn();

        if (Dial_Array[1, EndIndex] <= GetDialData().Index)
        {
            EndIndex++;
            GetDialData().NextDialIndex();
            EndDL();
        }
        else GetDialData().NextDialIndex();
    }


    public override void EndDL()
    {

        GetDialData().ToggleText(false);
        GetDialData().InActiveAnim();

        switch (EndIndex)
        {
            case 1: { EventBoxe.GetComponent<Collider2D>().enabled = true;
                    Sunhwa.SHMove();
                    UIManager.Instance.CreateAtCanvas(UIList.UI_ControlInfo, new Vector2(Screen.width / 2, Screen.height / 2)); //0
                    UIManager.Instance.CreateAtLocation(UIList.UI_Footstep, FootstepPosition[0].position); //1
                    UIManager.Instance.CreateAtLocation_Parent(UIList.UI_SpeechBubble, animator[0].transform.position + new Vector3(-2f, 2f, 0), animator[0].transform); //1

                }
                break;
            case 2:
                {
                    Start_CubicInteraction();
                } break;

          
            case 6: {
                    End_CubicInteraction();
                } break;
            case 7:
                {
                    Active_FadePanel();
                }
                break;
        }
    }


    private ShowDialData GetDialData()
    {
        return (ShowDialData)Data_Show[0];
    }
}

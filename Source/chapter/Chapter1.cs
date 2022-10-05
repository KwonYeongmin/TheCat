using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1 : ChapterManager
{
    [Header("Data Reader")]
    private ShowDialData Data_Show;

    [Header("Animator")]
    public Animator[] animator;

    [Header("Chapter1 GameObject")]
    public GameObject[] WaterTank;
    private EventBox[] EventBoxes;
    private Sunhwa Sunhwa;
    public GameObject FadeinoutPanel;


    [Header("Dialogue Length")]
    public int DialNum;
    public int[,] Dial_Array { get; private set; }
    public int[] EndDial; //4,16
    private int EndIndex = 0;


    [Header("Puzzle Interaction")]
    public puzzleController PuzzleController;

    [Header("Footstep Transform")]
    public Transform[] FootstepPosition;
    void Start()
    {
        PlayBGM(SoundList.Chapter1_Out);

        SoundManager.Instance.PlayUIAudio(SoundList.Sound_UnknowncCat);

        Data_Show = this.GetComponent<ShowDialData>();
        SetDial();

        EventBoxes = new EventBox[4];
        for (int i = 0; i < EventBoxes.Length; i++)
            EventBoxes[i] = GameObject.FindWithTag("EventBox").GetComponent < EventBox>();

        Sunhwa = GameObject.FindWithTag("Player").GetComponent<Sunhwa>();

        UIManager.Instance.CreateAtCanvas(UIList.UI_ControlInfo, new Vector2(Screen.width / 2, Screen.height / 2)); //0
        UIManager.Instance.CreateAtLocation(UIList.UI_Footstep, FootstepPosition[0].position); //1
        UIManager.Instance.CreateAtLocation_Parent(UIList.UI_SpeechBubble, animator[0].transform.position+new Vector3(-2f,2f,0),animator[0].transform); //1
    }

    private void Update()
    {
        TriggerEventBox();
    }

    // ================================================ Animation ================================================


    // ================================================ AnimationEvent ================================================

   // public Animator[] animator;
    public void Play_WaterboxCrack()
    {
        AnimationSetBool(animator[0], "BrokenGOGO", true);
        AnimationSetBool(animator[1], "isBoxBroken", true);
    }
    public void Play_WaterboxCrack_InActivePanel()
    {
        FadeinoutPanel.SetActive(false);
    }



    // ================================================ Function ================================================


    public override void TriggerEventBox()
    {
       
        if (Sunhwa.bIsTrigger)
        {
            int index = Sunhwa.eventBox.Index;

            switch (index)
            {
                case 0: {
                        UIManager.Instance.Destroy_UI(0);
                        StartDL();  } break;
                case 1: {
                        UIManager.Instance.Destroy_UI(2);
                        UIManager.Instance.Destroy_UI(3);

                        StartDL(); } break;
                case 2: { StartDL(); } break;
                case 3: { UIManager.Instance.ClearObjects();  ChangeScene(2); } break;
            }
            Sunhwa.eventBox.DestroyObject();
            Sunhwa.bIsTrigger = false;
        }
    }
  

    private void Start_PuzzleInteraction()
    {
        SoundManager.Instance.PlayBGM(SoundList.Chapter1_In);
        Data_Show.Panel[0].SetActive(true);
        PuzzleController.StartInteraction();
    }

    public void End_PuzzleInteraction()
    {
       

        AnimationSetBool(animator[0],"frontBoxGOGO",true);
        StartDL();
        PuzzleController.EndInteraction();
        
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

    private bool bIsHaveWaterTank = false;
    private int WaterTankIndex = 0;

    private void SetWaterTank()
    {
        if (EndIndex == 1)
        {
            bIsHaveWaterTank = true;
            WaterTankIndex = 0;
        }
        else if (EndIndex == 2)
        {
            bIsHaveWaterTank = true;
            WaterTankIndex = 1;
        }
        else bIsHaveWaterTank = false;
    }

    public override void StartDL()
    {
        base.StartDL();

        Data_Show.ToggleText(true);
        Data_Show.ShowText();

        SetWaterTank();
        Sunhwa.SHNotMove();
       if(bIsHaveWaterTank) WaterTank[WaterTankIndex].SetActive(true);
        else WaterTank[WaterTankIndex].SetActive(false);
    }

    public override void DownDialogueBtn()
    {
        base.DownDialogueBtn();
        if (Dial_Array[1, EndIndex] <= Data_Show.Index)
        {
            EndIndex++;
            Data_Show.NextDialIndex();
            WaterTank[WaterTankIndex].SetActive(false);
            EndDL();
          }
        else Data_Show.NextDialIndex();
    }


    public override void EndDL()
    {
       
        Data_Show.ToggleText(false);
        Data_Show.InActiveAnim();

        switch (EndIndex)
        {
            case 1: {
                    Sunhwa.SHMove();
                    UIManager.Instance.Destroy_UI(1);
                    UIManager.Instance.CreateAtLocation(UIList.UI_Footstep, FootstepPosition[1].position);

                }
                break;
            case 2:
                {
                    FadeinoutPanel.SetActive(true);
                    SoundManager.Instance.PlayUIAudio(SoundList.Sound_WatertankCrack); 

                } break;
            case 3:
                {
                    Start_PuzzleInteraction();

                } break;
            case 4:
                {
                    PuzzleController.Start_InteractionAnimation_Gogo();
                    Data_Show.Panel[0].SetActive(true);
                } break;
            case 5:
                {
                    PuzzleController.Start_InteractionAnimation_Sunhwa();
                    Data_Show.Panel[0].SetActive(true);
                }
                break;
            case 6: {
                    PuzzleController.End_InteractionAnimation();
                    Sunhwa.SHMove();
                    SoundManager.Instance.PlayBGM(SoundList.Chapter1_Out);
                    UIManager.Instance.CreateAtLocation(UIList.UI_Footstep, FootstepPosition[2].position);
                } break;
            case 7:
                {
                    UIManager.Instance.Destroy_UI(4);
                    UIManager.Instance.CreateAtLocation(UIList.UI_Footstep, FootstepPosition[3].position);

                    Sunhwa.SHMove();
                }
                break;

        }

    }
}

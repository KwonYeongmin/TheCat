using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : ChapterManager
{
    [Header("Data Reader")]
    public ShowData[] Data_Show;

    [Header("Animator")]
    public Animator[] animator; //  0: SH, 1 :Phone

    [Header("Itro GameObject")]
    public GameObject Phone_Screen;
    public GameObject SH_T;

    [Header("Star Interaction")]
    public Transform[] BG;
    public GameObject[] Interaction_Gameobject;


    [Header("Dialogue Length")]
    public int DialNum;
    public int[,] Dial_Array { get; private set; }
    public int[] EndDial; //4,16
    private int EndIndex = 0;


    public void Start()
    {
        PlayBGM(SoundList.Intro_Out);
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Radio);
        SetDial();
        bIsScroll_Interation = false;
        bIsStar_Interation = false;
        Data_Show[0].gameObject.SetActive(true);

        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Meteor);
        Invoke("StartRadio", 1f);
    }

    private void Update()
    {
        if (!bIsScroll_Interation) Scroll_PhoneScreen();
    }

    // ================================================ Animation ================================================

    // ================================================ Button_Animation ================================================
    // 라디오 끝난 후 선화 하늘 보기

    public void Play_SHAnim1()
    {
        ShowIllustData radio_data = (ShowIllustData)Data_Show[0];
       if (radio_data.bIsEnd) AnimationSetBool(animator[0], "SH_anim_1", true);
    }


    // ================================================ AnimationEvent ================================================
    public void Play_PhoneUp()
    {
        AnimationSetBool(animator[1], "phoneUp", true);
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Phew);
        SH_T.SetActive(true);
       // SNS 아이콘-> 스와이프 내려가기
    }

    public void Play_PhoneDown()
    {
        AnimationSetBool(animator[1], "phoneDown", true);
        SH_T.SetActive(false);
    }

    public void Play_SHAnim2()
    {
        AnimationSetBool(animator[0], "SH_anim_2", true);
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Navi);
    }

    public void Play_Swipe_Sound()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Swipe);
    }

    // ================================================ Function ================================================
    public GameObject Radio;

    private void StartRadio()
    {
        Radio.SetActive(true);
    }

    bool bIsScroll_Interation = false;
    bool bIsStar_Interation = false;

    public void Scroll_PhoneScreen()
    {
        float y = Phone_Screen.GetComponent<RectTransform>().position.y;

        GameObject[] SH_Ts = new GameObject[4];
        for (int i = 0; i < 4; i++)
            SH_Ts[i] = SH_T.transform.GetChild(i).gameObject;

        Animator[] SH_T_animtor = new Animator[4];
        for (int i = 0; i < 4; i++)
            SH_T_animtor[i] = SH_Ts[i].GetComponent<Animator>();

        if (y >= -0.375f * Screen.height)
        {
            SH_Ts[0].SetActive(true);
            SH_T_animtor[0].enabled = true;
        }

         if (y >= 0.15625f * Screen.height)
        {
            SH_Ts[1].SetActive(true);
            SH_T_animtor[1].enabled = true;
        }
         if (y >= 0.625f * Screen.height)
        {
            SH_Ts[2].SetActive(true);
            SH_T_animtor[2].enabled = true;
        }
         if (y >= 1.125f * Screen.height) 
        {
            SH_Ts[3].SetActive(true);
            SH_T_animtor[3].enabled = true;
            
            Invoke("Play_PhoneDown", 2f);
            bIsScroll_Interation = true;
        }
    }



    public void Start_StarInteraction()
    {
        PlayBGM(SoundList.Intro_In);

        BG[0].localScale = new Vector3(2f,2f,2f);
        BG[0].position = new Vector3(0.8f,-4.1f,1f);
        BG[1].localScale = new Vector3(0.42f,0.42f,1f);

        for (int i = 0; i < Interaction_Gameobject.Length-1; i++)
            Interaction_Gameobject[i].SetActive(true);

        Interaction_Gameobject[Interaction_Gameobject.Length - 1].SetActive(false);
    }

    public void End_StarInteraction()
    {
        Interaction_Gameobject[1].gameObject.SetActive(false);
        Interaction_Gameobject[3].gameObject.SetActive(false);
       
        StartDL();
    }

    //================================== Dialogue ==================================




    public void SetDial()
    {
        EndIndex = 0;
        Dial_Array = new int[2, DialNum];
      
        Dial_Array[0, 0] = 0;

        int count = 0;

        for (int i = 0; i < EndDial.Length; i++)
        {
            Dial_Array[1, i] = EndDial[i];

            if (i + 1 > EndDial.Length - 1) break ;
            Dial_Array[0, i + 1] = EndDial[i]+1;
            count++;
        }
    }

    public override void StartDL()
    {
        base.StartDL();
        GetDialData().ToggleText(true);
        GetDialData().ShowText();
    }

    public override void DownDialogueBtn()
    {
        if (Dial_Array[1, EndIndex]  <= GetDialData().Index)
        {
            EndIndex++;
            GetDialData().NextDialIndex();
            EndDL();
        } 
        else GetDialData().NextDialIndex();

        SoundManager.Instance.PlayUIAudio(SoundList.Sound_button);
    }

    public override void EndDL()
    {
        GetDialData().ToggleText(false);
        GetDialData().InActiveAnim();

        if (EndIndex == 1)
            Start_StarInteraction();
        else
            ChangeScene(1);
    }

    private ShowDialData GetDialData()
    {
        return (ShowDialData)Data_Show[1];
    }
}

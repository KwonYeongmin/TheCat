using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeInteraction : Interaction
{
    enum InteractionState { Spinx, Sunhwa };
    InteractionState state = InteractionState.Spinx;

    public bool bIsInteractionStart = false;
    
   [Header("Stone")]
    public GameObject stone;

    GameObject[] Stones = new GameObject[4];
    GameObject[] Stone_Stokes = new GameObject[4];
    Collider2D[] Stone_Collider = new Collider2D[4];

    [Header("Spinx")]
    public GameObject Spinx;
    GameObject[] Spinx_Images = new GameObject[4];


    [Header("Sunhwa")]
    public GameObject Sunhwa;
    GameObject[] Sunhwa_Images = new GameObject[4];

    public int GetMode() { return (int)state; }

    private void Start()
    {
        state = InteractionState.Spinx;


        for (int i = 0; i < Stones.Length; i++)
        {
            Stones[i] = stone.transform.GetChild(i).gameObject;
            Stone_Collider[i] = Stones[i].GetComponent<Collider2D>();
            Stone_Stokes[i] = Stones[i].transform.GetChild(0).gameObject;
        }

        for (int i = 0; i < 4; i++)
        {
            Spinx_Images[i] = Spinx.transform.GetChild(i).gameObject;
            Sunhwa_Images[i] = Sunhwa.transform.GetChild(i).gameObject;
        }

        Reset();
    }


    public void Reset()
    {

        // Stone
        ResetGameobjects(Stone_Stokes);


        // collider
        Stone_Collider[1].enabled = true;
       for(int i=2;i<4;i++) Stone_Collider[i].enabled = false;

        // images
        switch (state)
        {
            case InteractionState.Spinx:
                {
                    ResetGameobjects(Spinx_Images);
                    for (int i = 0; i < Sunhwa_Images.Length; i++)
                        Sunhwa_Images[i].SetActive(false);
                }
                break;
            case InteractionState.Sunhwa:
                {
                    ResetGameobjects(Sunhwa_Images); 
                    for (int i = 0; i < Spinx_Images.Length; i++)
                        Spinx_Images[i].SetActive(false);
                }
                break;

        }

        // btn
        GameManager.Instance.GetChapter2().Bridge_Interaction_Btn.SetActive(false);
    }

    private void ResetGameobjects(GameObject[] objects)
    {
        objects[0].SetActive(true);
        for (int i = 1; i < 4; i++)
        {
            objects[i].SetActive(false);
        }
    }

    public override void StartInteraction()
    {
        
        SoundManager.Instance.PlayBGM(SoundList.Chapter2_In);
        bIsInteractionStart = true;
        UIManager.Instance.CreateAtLocationToFront(UIList.UI_Footstep, Stones[1].transform.position);
        switch (state)
        {
            case InteractionState.Spinx:
                {
                    ResetGameobjects(Spinx_Images);
                    for (int i = 0; i < Sunhwa_Images.Length; i++)
                        Sunhwa_Images[i].SetActive(false);
                } break;
            case InteractionState.Sunhwa:
                {
                    ResetGameobjects(Sunhwa_Images);
                    for (int i = 0; i < Spinx_Images.Length; i++)
                        Spinx_Images[i].SetActive(false);
                } break;
        }

    }


    public override void EndInteraction()
    {
      Chapter2 chapter2 =   GameManager.Instance.GetChapter2();

        switch (state)
        {
            case InteractionState.Spinx:
                {
                    chapter2.Bridge_Interaction_Btn.SetActive(true);
                    state = InteractionState.Sunhwa;
                } break;

            case InteractionState.Sunhwa:
                {
                    chapter2.Bridge_Interaction_Btn.SetActive(true);
                    //  chapter2.LoadingWindow.SetActive(true);
                    // chapter2.ChangeScene(3);
                } break;
        }

        SoundManager.Instance.PlayBGM(SoundList.Chapter2_Out);
        bIsInteractionStart = false;
    }


    public void TouchEvent()
    {
        ClickInteraction clickInteraction = GameManager.Instance.GetChapter2().clickInteraction;
        
       // if (clickInteraction.bIsCkickTrigger)
        {
            GameObject target = clickInteraction.ClickTarget;
            int index = 2;

            if (target.GetComponent<ClickObject>())
                index = target.GetComponent<ClickObject>().Index;

            


            // Spinx
            switch (state)
            {
                case InteractionState.Spinx:
                    {
                        Spinx_Images[index - 2].SetActive(false);
                        Spinx_Images[index - 1].SetActive(true);
                        if (index == 2)
                            UIManager.Instance.Destroy_UI(7);
                    }
                    break;
                case InteractionState.Sunhwa:
                    {
                        Sunhwa_Images[index - 2].SetActive(false);
                        Sunhwa_Images[index - 1].SetActive(true);
                        if (index == 2)
                            UIManager.Instance.Destroy_UI(8);
                    }
                    break;
            }
          
            

            //stonestroke
            Stone_Stokes[index - 2].SetActive(false);
            Stone_Stokes[index - 1].SetActive(true);

            // collider
            if (index != 4) Stone_Collider[index].enabled = true;
            else EndInteraction();


            // feedback
            SoundManager.Instance.PlayUIAudio(SoundList.Sound_stone[index - 2]);
           

        }
    }
}

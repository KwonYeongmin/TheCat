using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainInteraction : Interaction
{
    enum InteractionState { Umbrella, Clock };
    InteractionState state = InteractionState.Umbrella;

    [Header("Umbrella Interaction")]
    public GameObject Umbrella;

    [Header("Umbrella Interaction")]
    public GameObject Clock_Panel;
    public GameObject Clock;

    public bool bIsUmbrellaEnd = false;
    public bool bIsClockEnd = false;

    private void Start()
    {
        state = InteractionState.Umbrella;
        bIsUmbrellaEnd = false;
        bIsClockEnd = false;
    }
    public override void StartInteraction()
    {
        switch (state)
        {
            case InteractionState.Umbrella:
                {
                 
                } break;
            case InteractionState.Clock:
                {
                    Clock_Panel.SetActive(true);
                }
                break;
        }
    }



    public override void EndInteraction() 
    {
        switch (state)
        {
            case InteractionState.Umbrella:
                {
                    Umbrella.GetComponent<Collider2D>().enabled = false;
                    Umbrella.GetComponent<Animator>().SetBool("IsUbrellaOpen", true);

                    state = InteractionState.Clock;
                    // StartInteraction();

                }
                break;
            case InteractionState.Clock:
                {
                    Clock_Panel.SetActive(false);

                    // 싱글톤에 넣기
                    //GetComponent<ChapterManager>();
                    GameObject.FindWithTag("Chapter").GetComponent<Chapter2>().End_RainInteraction();
                   
                    
                    // base.EndInteraction();
                } break;
        }
    }


}

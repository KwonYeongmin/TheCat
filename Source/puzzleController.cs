using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class puzzleController : Interaction
{

 
    public GameObject WatertankBG;
    public GameObject InteractionPanel;
    public GameObject InteractionButton;

    public int puzzleCount = 0;


    public GameObject Pieces;
    public GameObject Slots;


    private GameObject[] puzzlePieces;
    private GameObject[] puzzleSlots;
    private GameObject[] GogoFace;
    private GameObject[] SHFace;

    void Start()
    {
        puzzlePieces = new GameObject[5];
        puzzleSlots = new GameObject[5];
        GogoFace = new GameObject[5];
        SHFace = new GameObject[5];

        for (int i = 0; i < 5; i++)
        {
            puzzlePieces[i] = Pieces.transform.GetChild(i).GetChild(0).gameObject;
            puzzleSlots[i] = Pieces.transform.GetChild(i).gameObject;

            SHFace[i] = puzzlePieces[i].transform.GetChild(0).gameObject;
            GogoFace[i] = puzzlePieces[i].transform.GetChild(1).gameObject;
        }
          


        ToggleSlots(false);
        TogglePieces(false);
        ToggleGogoFace(false);
        ToggleSunhwaFace(false);
        InteractionPanel.SetActive(false);
        InteractionButton.SetActive(false);
    }

    private void Update()
    {
        if (!bIsEndInteraction)
            if (puzzleCount >= 5) InteractionButton.SetActive(true);

       // Debug.Log();
    }

    private void ToggleGameobject(GameObject[] objects,bool value)
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].SetActive(value);
    }

    // Slot
    public void ToggleSlots(bool value)
    {
        ToggleGameobject(puzzleSlots, value);
    }

    // Pieces
    public void TogglePieces(bool value)
    {
        ToggleGameobject(puzzlePieces, value);
    }

    //GogoFace
    public void ToggleGogoFace(bool value)
    {
        ToggleGameobject(GogoFace, value);

        for (int i = 0; i < SHFace.Length; i++)
            if (GogoFace[i].GetComponent<Animator>())
                GogoFace[i].GetComponent<Animator>().enabled = value;
    }

    //SunhwaFace
    public void ToggleSunhwaFace(bool value)
    {
        ToggleGameobject(SHFace, value);
        for(int i=0;i<SHFace.Length;i++)
            if (SHFace[i].GetComponent<Animator>())
                SHFace[i].GetComponent<Animator>().enabled = value;
    }


    //사이즈 늘이기
    public void sizeUp()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            Vector3 size =  puzzlePieces[i].GetComponent<RectTransform>().localScale * 1.2f;
            puzzlePieces[i].GetComponent<RectTransform>().localScale = size;
        }
         

    }



    //애니메이션 변수 true로
    public void SH_OpenEyes()
    {
        for (int i = 0; i < puzzlePieces.Length; i++) { this.SHFace[i].GetComponent<Animator>().SetBool("SHEyes", true); }

    }



    public override void StartInteraction()
    {
        InteractionPanel.SetActive(true);
        TogglePieces(true);
        ToggleSlots(true);
    }

    public override void EndInteraction()
    {
        WatertankBG.SetActive(false);
        sizeUp();
       
        InteractionPanel.SetActive(false);
        base.EndInteraction();
    }

    public void Start_InteractionAnimation_Gogo()
    {
        InteractionPanel.SetActive(true);
        ToggleGogoFace(true);
    }

    public void Start_InteractionAnimation_Sunhwa()
    {
        ToggleGogoFace(false);
        ToggleSunhwaFace(true);
    }

    public void End_InteractionAnimation()
    {
        InteractionPanel.SetActive(false);
    }
}

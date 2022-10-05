using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : ChapterManager
{
    public GameObject LanguageSelectionPanel;
   

    private void Start()
    {
        GameManager.Instance.Init();
        SoundManager.Instance.PlayBGM(SoundList.MainBGM);
    }


    public void ToggleLanguageSelectionPanel(bool value)
    {
        LanguageSelectionPanel.SetActive(value);
    }
    
}

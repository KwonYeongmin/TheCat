using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelection : ChapterManager
{
    private void Start()
    {
      //  PlayBGM(SoundList.MainBGM);
    }


    public void PlayBtnSFX()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_button);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OptionWindow.Instance.OpenOptionWindow();
    }
}

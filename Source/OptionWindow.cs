using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionWindow : Singleton<OptionWindow>
{
    public Slider[] soundController;
    public Button[] versionController;
    //public Button OkBtn;
    private GameObject Option_Window;
    private float DefaultTimeScale;
    private bool bIsOptionOn=false;
    public AudioMixerGroup Master;
    public AudioMixerGroup BGM;
    public AudioMixerGroup SFX;

    private void Start()
    {
       
        Option_Window = this.transform.GetChild(0).gameObject;
        DefaultTimeScale = Time.timeScale;
        bIsOptionOn = false;

        ControlMasterAudio();
        ControlBGMAudio();
        ControlSFXAudio();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!bIsOptionOn)
                OpenOptionWindow();
            else
                CloseOptionWindow();
        }
    }

    public void CloseOptionWindow()
    {
        Time.timeScale = DefaultTimeScale;
        bIsOptionOn = false;
        Option_Window.SetActive(false);
    }

    public void OpenOptionWindow()
    {
        Time.timeScale = 0;
        bIsOptionOn = true;
        Option_Window.SetActive(true);
    }

    // ====================================== Version
    public void SelectVersion(int version)
    {
        GameManager.Instance.SetLanguageVersion(version);
        GameManager.Instance.DataReader.ReadNewChapterData();
    }

    // ====================================== Audio
    public void ControlMasterAudio()
    {
        if (bIsOptionOn)
        {
            float value =  soundController[0].value <= -20 ?  0: soundController[0].value;
            Master.audioMixer.SetFloat("MasterVolume", value);
            
        }
           
    }

    public void ControlBGMAudio()
    {
      
        if (bIsOptionOn)
        {
            float value = soundController[1].value <= -20 ? 0 : soundController[1].value;
            BGM.audioMixer.SetFloat("BGMVolume", value);
        }

    }
    public void ControlSFXAudio()
    {
        if (bIsOptionOn)
        {
            float value = soundController[2].value <= -20 ? 0 : soundController[2].value;
            SFX.audioMixer.SetFloat("SFXVolume", value);
        }
    }


    public void DownMainButton()
    {
        GameManager.Instance.ChangeScene(-2);
    }



    public void ToggleOptionWindow(bool value)
    {
        Option_Window.SetActive(value);
    }
}

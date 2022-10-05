using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    ChapterManager Chapter;

    private void Start()
    {
        Chapter = GameObject.FindWithTag("Chapter").GetComponent<ChapterManager>();
    }
    // ===================================================================================
    public void SetInActive()
    {
        this.gameObject.SetActive(false);
    }

    public void  StartDL()
    {
        Chapter.StartDL();
    }
    // ====================================== Intro ======================================


    public void Play_SHAnim1()
    {
        if (GetIntroChapter()) GetIntroChapter().Play_SHAnim1();
    }

    public void Play_PhoneUp()
    {
        if (GetIntroChapter()) GetIntroChapter().Play_PhoneUp();
    }

    public void Play_PhoneDown()
    {
        if (GetIntroChapter()) GetIntroChapter().Play_PhoneDown();
    }

    public void Play_SHAnim2()
    {
        if (GetIntroChapter()) GetIntroChapter().Play_SHAnim2();
    }

    public void Play_Swipe_Sound()
    {
        if (GetIntroChapter()) GetIntroChapter().Play_Swipe_Sound();
    }

    // ====================================== Chapter1 ======================================

    public void Play_WaterboxCrack()
    {
        if (GetChapter1Chapter()) GetChapter1Chapter().Play_WaterboxCrack();
    }

    public void Play_SoundEyes_Gogo()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Gogo_Eyeopen);
    }
    public void Play_SoundEyes_Sunhwa()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Sunhwa_Eyeopen);
    }

    // ====================================== Chapter2 ========================================
    public void Play_ClickUI()
    {
        if (GetChapter2()) GetChapter2().Play_ClickUI();
    }

    public void TurnOn_SceneAnim(int index)
    {
        if (GetChapter2()) GetChapter2().Toggle_SceneAnimation(index, true);
        
    }
    public void Play_SceneAnimation()
    {
      Chapter.Play_Fadeinout();
    }

    public void End_SceneAnimation()
    {
        Chapter.End_SceneAnimation();
        SetInActive();
    }

    public void Active_FadePanel()
    {
       Chapter.Active_FadePanel();
    }
    public void Play_ClockInteraction()
    {
        if (GetChapter2()) GetChapter2().Play_ClockInteraction();
    }

    public void Play_SpinxSound()
    {
        if (GetChapter2()) GetChapter2().Play_SpinxSound();
    }

    public void Play_SunhwaTailSound()
    {
        if (GetChapter2()) GetChapter2().Play_SunhwaTailSound();
    }


    // ====================================== Chapter3 ========================================


    public void Play_RainbowScene()
    {
        if (GetChapter3()) GetChapter3().Play_RainbowScene();
    }
    public void Play_CubicParticle()
    {
        if (GetChapter3()) GetChapter3().Play_CubicParticle();

    }

    public void Move_CameraView()
    {
        if (GetChapter3()) GetChapter3().Move_CameraView();

    }
    public void Play_GlowCatStar()
    {
        if (GetChapter3()) GetChapter3().Play_GlowCatStar();

    }





    // ====================================== GetChapter ==================================
    public Intro GetIntroChapter()
    {
        return (Intro)Chapter;
    }

    public Chapter1 GetChapter1Chapter()
    {
        return (Chapter1)Chapter;
    }

    public Chapter2 GetChapter2()
    {
        return (Chapter2)Chapter;
    }
    public Chapter3 GetChapter3()
    {
        return (Chapter3)Chapter;
    }
}

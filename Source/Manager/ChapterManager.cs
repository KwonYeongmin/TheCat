using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public GameObject LoadingWindow;

    protected void PlayBGM(AudioClip BGM)
    {
        SoundManager.Instance.PlayBGM(BGM);
    }

    public void ChangeScene(int chapterIndex)
    {
        LoadingWindow.SetActive(true);
        GameManager.Instance.ChangeScene(chapterIndex);
    }

    public void SelectVersion(int version)
    {
        GameManager.Instance.SetLanguageVersion(version);
    }

    // animtion bool
    public void AnimationSetBool(Animator animator, string boolValue, bool value)
    {
        animator.SetBool(boolValue, value);
    }

    public virtual void StartDL()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_DialUp);
    }
    public virtual void DownDialogueBtn()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_DialDown);
    }
    public virtual void EndDL() { }

    public virtual void Play_StoryAnimation() { }
    public virtual void ClickObject() { }

    public virtual void TriggerEventBox() { }
    public virtual void Play_Fadeinout() { }

    public virtual void End_SceneAnimation() { }

    public virtual void Active_FadePanel() { }

    public void DownBtn_Audio()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_button);
    }
    public void DownArrowBtn_Audio()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Arrowbutton);

    }
}

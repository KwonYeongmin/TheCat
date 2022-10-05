using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameManager() { }
    public int LanguageVersion { get; private set; }
    public void SetLanguageVersion(int index) { LanguageVersion = index; }
    public int Chapter { get; private set; }
    public ReadData DataReader { get; private set; }


    public void Init()
    {
        Chapter = -2;
        LanguageVersion = 0;
        DataReader = new ReadData();
    }


    public void ChangeScene(int chapterIndex)
    {
        Chapter = chapterIndex;
        SceneManager.LoadScene(chapterIndex + 2);
        DataReader.ReadNewChapterData();
    }



    public Intro GetIntro()
    {
        return GameObject.FindWithTag("Chapter").GetComponent<Intro>();
    }

    public Chapter1 GetChapter1()
    {
        return GameObject.FindWithTag("Chapter").GetComponent<Chapter1>();
    }

    public Chapter2 GetChapter2()
    {
        return GameObject.FindWithTag("Chapter").GetComponent<Chapter2>();
    }

    public Chapter3 GetChapter3()
    {
        return GameObject.FindWithTag("Chapter").GetComponent<Chapter3>();
    }
}

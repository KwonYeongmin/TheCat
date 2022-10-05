using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class video_url : MonoBehaviour
{
    public string url_name = "title_motion.mp4";
    public GameObject EndingScene;
    VideoPlayer player;
    public GameObject btn;

    void Start()
    {

        player = this.gameObject.GetComponent<VideoPlayer>();
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, url_name);
        player.url = filePath;

    }



    private void Update()
    {
        if (player.isPaused)
            EndingScene.SetActive(true);
    }

    public void DownSkipButton()
    {
        player.Stop();
        EndingScene.SetActive(true);
        btn.SetActive(false);
    }
}

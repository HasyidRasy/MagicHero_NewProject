using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayIntro : MonoBehaviour {
    public VideoPlayer videoPlayer;


    void Awake() {
        StartCoroutine(StreamVideo());
    }

    private IEnumerator StreamVideo() {
        videoPlayer.Play();

        while (videoPlayer.isPlaying) {
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }
}

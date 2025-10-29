using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class MessageManager : MonoBehaviour
{
    [SerializeField] private MessageHolder messageHolder;
    [SerializeField] private RawImage videoCanvas;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RenderTexture videoTexture;

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            PlayVideo("blue");
        }

        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            pauseVideo();
        }
    }

    void PlayVideo(string MessageID)
    {
        foreach (MessageHolder.VideoEntry key in messageHolder.videoList)
        {
            if (key.key == MessageID)
            {
                videoPlayer.clip = key.clip;
                videoPlayer.Play();
            }
            else
            {
                Debug.Log("Key not found!");
                return;
            }
        }
    }

    void pauseVideo()
    {
        if (!videoPlayer.isPaused)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }
    
    void StopVideo()
    {
        videoPlayer.Stop();
        videoPlayer.clip = null;
    }
    
}

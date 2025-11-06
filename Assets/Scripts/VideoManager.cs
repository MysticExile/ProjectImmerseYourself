using UnityEngine;
using UnityEngine.InputSystem;   // New Input System namespace
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;   // Playlist
    private int currentIndex = 0;

    // Input Actions
    [Header("Input")]
    public InputActionAsset actionsAsset;
    public string actionMapName = "UI";
    public string navigateActionName = "Navigate";
    public string submitActionName = "Submit";
    public string nextSceneName;

    private InputAction submitAction;

    void Awake()
    {
        // Get PlayerInput component (attach one in Inspector)
        var map = actionsAsset.FindActionMap(actionMapName, true);
        submitAction = map.FindAction(submitActionName, true);
    }

    void Start()
    {
        if (videoPlayer != null && videoClips.Length > 0)
        {
            videoPlayer.clip = videoClips[currentIndex];
            videoPlayer.Play();

            // Subscribe to loopPointReached (fires when video ends)
            videoPlayer.loopPointReached += OnVideoEnd;
        }

        // Bind input callbacks
        submitAction.performed += ctx => NextVideo();
    }

    void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
        else
            videoPlayer.Play();
    }

    void NextVideo()
    {
        if (videoClips.Length == 0) return;

        currentIndex++;

        if (currentIndex >= videoClips.Length)
        {
            // If last video has finished, go to next scene
            LoadNextScene();
        }
        else
        {
            videoPlayer.clip = videoClips[currentIndex];
            videoPlayer.Play();
        }
    }

    void PreviousVideo()
    {
        if (videoClips.Length == 0) return;

        currentIndex = (currentIndex - 1 + videoClips.Length) % videoClips.Length;
        videoPlayer.clip = videoClips[currentIndex];
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Automatically play next video when current ends
        NextVideo();
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name not set!");
        }
    }
}

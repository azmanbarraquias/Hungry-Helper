using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RecipeTTP : MonoBehaviour
{
    public TextMeshProUGUI contentTMP;

    [Range(-10, 10)]
    [Tooltip("The speech rate (speed). Allows values: from -10 (slowest speed) up to 10 (fastest speed). Default value: 0 (normal speed)")]
    public float speed = -2;


    public AudioSource audioSource;

    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject loadingUI;

    public AudioClip audioClip;

    public bool hasRun = false;

    private string apiKey = "521685a4592548228f2d9cf2a0f54ded";

    private string _url;

    public GameObject noInternetUI;

    private void OnEnable()
    {
        if (audioSource.clip != null)
        {
            audioSource.clip = audioClip;
        }

        if (audioClip == null)
        {
            hasRun = false;
        }
       

        playButton.SetActive(true);
        pauseButton.SetActive(false);
        loadingUI.SetActive(false);
    }

    private void OnDisable()
    {
        //audioSource.clip = null; // put on exit
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void PlayButton()
    {
        if (hasRun == false && audioClip == null)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                noInternetUI.SetActive(true);
                return;
            }
            hasRun = true;
            StartCoroutine(RequestTextToSpeech());
        }
        else
        {
            if (audioSource.clip == null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();

                pauseButton.SetActive(true);
                playButton.SetActive(false);
            }
            else
            {
                audioSource.Play();

                pauseButton.SetActive(true);
                playButton.SetActive(false);
            }
        }
    }

    public void PauseButton()
    {
        audioSource.Pause();

        pauseButton.SetActive(false);
        playButton.SetActive(true);
    }

    public void ResetAudio()
    {
        hasRun = false;

        audioClip = null;
        audioSource.clip = null;

        playButton.SetActive(true);
        pauseButton.SetActive(false);
        loadingUI.SetActive(false);
    }

    private IEnumerator RequestTextToSpeech()
    {
        playButton.SetActive(false);
        loadingUI.SetActive(true);

        _url = "https://voicerss-text-to-speech.p.rapidapi.com/?r=" + speed + "&c=wav&f=8khz_8bit_mono&src=" + contentTMP.text + "&hl=en-us&key=" + apiKey;

        UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(_url, AudioType.WAV);

        webRequest.SetRequestHeader("x-rapidapi-host", "voicerss-text-to-speech.p.rapidapi.com");
        webRequest.SetRequestHeader("x-rapidapi-key", "69c64357e6mshfafd205e447e98bp1b0617jsn84b39a79606d");

        yield return webRequest.SendWebRequest();

        if (!webRequest.isNetworkError && !webRequest.isHttpError)
        {
            DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)webRequest.downloadHandler;
            if (dlHandler.isDone)
            {
                loadingUI.SetActive(false);
                pauseButton.SetActive(true);

                audioClip = dlHandler.audioClip;

                audioSource.clip = audioClip;
                audioSource.Stop();
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError(webRequest.error);
            ResetAudio();
        }
    }
}

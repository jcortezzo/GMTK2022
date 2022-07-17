using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    public GameConfig config;
    public Image progressImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (config.InifiniteMode) return;
        AudioSource audioSource = Jukebox.Instance.GetMusicCurrentAudioSource();
        float maxTime = audioSource.clip.length;
        progressImage.fillAmount = audioSource.time / maxTime;
    }
}

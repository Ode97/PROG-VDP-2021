using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    public Slider musicSlider;
    private float lastMusicVal;
    public AudioSource music;
    public Button musicButton;
    public Sprite MusicOn;  
    public Sprite MusicOff;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = 1f;
        musicButton.onClick.AddListener(mute);
    }

    // Update is called once per frame
    void Update()
    {
        musicSlider.onValueChanged.AddListener(delegate {changeVolume ();});
    }

    void changeVolume(){
        music.volume = musicSlider.value * 0.5f;
        if(musicSlider.value < 0.2){
            musicButton.GetComponent<Image>().sprite = MusicOff;
        }
        else {
            musicButton.GetComponent<Image>().sprite = MusicOn;
        }
    }
    void mute() {
        if (music.volume == 0){
            musicButton.GetComponent<Image>().sprite = MusicOn;
            music.volume = lastMusicVal* 0.5f;
            musicSlider.value = lastMusicVal;
        }
        else {
            musicButton.GetComponent<Image>().sprite = MusicOff;
            lastMusicVal = musicSlider.value;
            music.volume = 0; 
            musicSlider.value = 0;
        }
    }
}

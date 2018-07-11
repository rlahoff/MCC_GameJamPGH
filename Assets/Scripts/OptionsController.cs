using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public Slider musicVolumeSlider;         // drag into inspector from hierarchy
    public Slider sfxVolumeSlider;         // drag into inspector from hierarchy
    public Slider difficultySlider;

    public LevelManager levelManager;
    private MusicManager musicManager;

    // Use this for initialization
    void Start () {
        musicManager = FindObjectOfType<MusicManager>();
        if (!musicManager)
            Debug.LogWarning("MusicManager not found");

        musicVolumeSlider.value = PPrefsMgr.GetMusicVolume();
        sfxVolumeSlider.value   = PPrefsMgr.GetSfxVolume();
        difficultySlider.value  = PPrefsMgr.GetDifficulty();
	}
	
	// Update is called once per frame
	void Update () {
        if (musicManager) musicManager.SetVolume(musicVolumeSlider.value);
    }

    public void ResetToDefaults()
    {
        musicVolumeSlider.value = 0.8f;
        sfxVolumeSlider.value = 0.8f;
        difficultySlider.value = 2f;
    }

    public void SaveAndExit()
    {
        PPrefsMgr.SetMusicVolume(musicVolumeSlider.value);
        PPrefsMgr.SetSfxVolume(sfxVolumeSlider.value);
        PPrefsMgr.SetDifficulty(difficultySlider.value);
        levelManager.LoadLevel("Start");
    }
}

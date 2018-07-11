using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PPrefsMgr : MonoBehaviour {

    const string MUSIC_VOLUME_KEY = "music_volume";
    const string SFX_VOLUME_KEY = "sfx_volume";
    const string DIFFICULTY_KEY = "difficulty";
 //   const string LEVEL_KEY = "level_unlocked_"; // level_unlocked_0, level_unlocked_1, etc.

    private static float sfxVolume;   // save sfxVolume for use by game so we're not pulling from disk all the time

    public static void SetMusicVolume(float volume)
    {
        if (volume >= 0 && volume <= 1)
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        else
            Debug.LogError("master volume out of range: " + volume);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }

    public static float GetSfxVolume()
    {
        return sfxVolume;
    }

    public static void SetSfxVolume(float volume)
    {
        if (volume >= 0 && volume <= 1)
        {
            sfxVolume = volume;
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        }
        else
            Debug.LogError("master volume out of range: " + volume);
    }

    public static float GetSfxVolumeFromDisk()
    {
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
        return sfxVolume;
    }

    /*    public static void UnlockLevel(int level)
        {
            if (level < SceneManager.sceneCountInBuildSettings)
                PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);    // 1 is true
            else
                Debug.LogError("level out of range: " + level);
        }

        public static bool IsLevelUnlocked(int level)
        {
            if (level < SceneManager.sceneCountInBuildSettings)
            {
                return (PlayerPrefs.GetInt(LEVEL_KEY + level.ToString()) == 1);    // 1 is true
            }
            else
            {
                Debug.LogError("level out of range: " + level);
                return false;
            }
        }
    */
    public static void SetDifficulty(float difficulty)
    {
        if (difficulty >= 1 && difficulty <= 3)
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        else
            Debug.LogError("difficulty out of range (1 to 3): " + difficulty);
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }

}

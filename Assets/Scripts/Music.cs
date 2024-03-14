using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music music;

    private void Awake()
    {
        if (music != null)
        {
            Destroy(gameObject);
        }
        else
        {
            if (PlayerPrefs.GetString("Music") == "Off")
            {
                AudioListener.volume = 0f;
            }
            else if(PlayerPrefs.GetString("Music") == "On")
            {
                AudioListener.volume = 1f;
            }
            music = this;
            DontDestroyOnLoad(transform.gameObject);
        }

    }
}

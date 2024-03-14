using UnityEngine.SceneManagement;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private int buttonNum;
    static private bool musicPlaying = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetString("Music") == "Off")
        {
            musicPlaying = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            if (buttonNum == 1)
            {
                PlayerPrefs.DeleteKey("DoodleRec");
                PlayerPrefs.DeleteKey("FallingRec");
            }
            else if (buttonNum == 2)
            {
                if (musicPlaying == true)
                {
                    PlayerPrefs.SetString("Music", "Off");
                    AudioListener.volume = 0f;
                    musicPlaying = false;
                }
                else if (musicPlaying == false)
                {
                    PlayerPrefs.SetString("Music", "On");
                    AudioListener.volume = 1f;
                    musicPlaying = true;
                }
            }
            PlayerPrefs.Save();
            SceneManager.LoadScene("Settings");
        }

    }
}

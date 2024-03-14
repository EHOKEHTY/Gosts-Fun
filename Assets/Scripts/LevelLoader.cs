using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int level;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && level >= 0)
        {
            SceneManager.LoadScene(level);  
        }
        else if (collider.gameObject.tag == "Player" && level < 0)
        {
            Application.Quit();
        }
    }
}

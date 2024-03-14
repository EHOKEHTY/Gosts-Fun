using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Doodle : MonoBehaviour
{
    [SerializeField] private float JumpForse = 15.0f;
    float timer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.relativeVelocity.y <= 0)
            {
                Hero.Instance.DooddleJump(JumpForse);
            }       
        }
    }
}

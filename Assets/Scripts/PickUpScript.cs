using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField] int addScore;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Hero.Instance.Score(addScore);
            Destroy(gameObject);
        }
    }
}
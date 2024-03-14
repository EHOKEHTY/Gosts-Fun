using UnityEngine;

public class HourGlass : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Camera.Instance.CameraSpeedDown(0.3f);
            Destroy(gameObject);
        }
    }
}

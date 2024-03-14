using UnityEngine.SceneManagement;
using UnityEngine;


public class Camera : MonoBehaviour
{
    public static Camera Instance { get; set; }

    [SerializeField] private float dumping = 3.0f;
    [SerializeField] private float offset;
    [SerializeField] private float camSpeed = 3.0f;
    [SerializeField] private float downSpeed;

    private bool playerRight = true;
    private bool gameStarted = false;
    private float lastPos;
    private const float OffsetX = 1.0f;
    private const float OffsetY = 1.0f;
    private Vector2 vecOffset = new Vector2(OffsetX, OffsetY);
    private Scene activeScene;
    private Transform player;

    private void Awake()
    {
        Instance = this;
        activeScene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        vecOffset = new Vector2(Mathf.Abs(vecOffset.x), vecOffset.y);
        FindePlayer(playerRight);
    }

    void FindePlayer(bool playerRight)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPos = player.position.x;

        if (playerRight)
        {
            transform.position = new Vector3(player.position.x + vecOffset.x, player.position.y + vecOffset.y, transform.position.z + 5);
        }
        else
        {
            transform.position = new Vector3(player.position.x - vecOffset.x, player.position.y + vecOffset.y, transform.position.z + 5);
        }
    }

    private void Update()
    {
        if (Hero.Instance != null)
        {
            if (!gameStarted)
            {
                if (activeScene.name == "Falling")
                {
                    if (Hero.Instance.transform.position.x < 2.5f && Hero.Instance.transform.position.x > 0.5f)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(0.0f, 0.0f, 0.0f), dumping / 2 * Time.deltaTime);
                    }
                    else
                    {
                        StandartCameraMove();
                    }
                }
                else if (activeScene.name == "Doodle")
                {
                    if (Hero.Instance.transform.position.x > -0.83f)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(0.0f, -1.3f, -10.0f), dumping / 2 * Time.deltaTime);
                    }
                    else
                    {
                        StandartCameraMove();
                    }
                }
                else
                {
                    StandartCameraMove();
                }
                if (activeScene.name == "Doodle" && Hero.Instance.transform.position.y > 1f)
                {
                    transform.position = new Vector3(0.0f, 0.0f, -10.0f);
                    gameStarted = true;
                }
                else if (activeScene.name == "Falling" && Hero.Instance.transform.position.y < -1f)
                {
                    transform.position = new Vector3(0.0f, -1.3f, -10.0f);
                    gameStarted = true;
                }
            }
            else if (gameStarted && activeScene.name == "Doodle")
            {
                float currentY = player.position.y;

                if (currentY > lastPos)
                {
                    transform.position = new Vector3(transform.position.x, player.position.y + offset, transform.position.z);
                    lastPos = player.position.y;
                }
            }
            else if (gameStarted && activeScene.name == "Falling")
            {
                FallingCameraMove();
            }
        }
    }
    private void FallingCameraMove()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - camSpeed * Time.deltaTime);
        camSpeed += downSpeed * Time.deltaTime;
        if (Hero.Instance.transform.position.y < transform.position.y - 1.5f)
        {
            transform.position = new Vector3(transform.position.x, Hero.Instance.transform.position.y + 1.5f);
        }
    }
    public void CameraSpeedDown(float speedDown)
    {
        camSpeed *= speedDown;
    }

    void StandartCameraMove()
    {
        float currentX = player.position.x;
        if (player)
        {
            if (currentX < lastPos)
            {
                playerRight = false;
            }
            else
            {
                playerRight = true;
            }
            lastPos = player.position.x;

            Vector3 target;
            if (playerRight)
            {
                target = new Vector3(player.position.x + vecOffset.x, player.position.y + vecOffset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x - vecOffset.x, player.position.y + vecOffset.y, transform.position.z);

            }
            transform.position = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
        }
    }

}

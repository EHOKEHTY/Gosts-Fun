using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Hero : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float JumpForse = 3.0f;
    [SerializeField] private Transform groundCheck;
    [Header("For game")]
    [SerializeField] private GameObject ScoreText;
    [SerializeField] private GameObject RecordText;

    private Scene activeScene;
    private Collider2D collider;
    private Rigidbody2D rigidbody;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool isGrounded;
    private int score;
    private int record;
    private int live = 1;
    public static Hero Instance { get; set; }

    public void Damage(int dam)
    {
        live -= dam;
        if (live == 0)
        {
            Destroy(this);
        }
    }

    private void Awake() //получение доступа
    {
        activeScene = SceneManager.GetActiveScene();
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Instance = this;
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        UnityEngine.Cursor.visible = false;

        if (RecordText != null && ScoreText != null)
        {
            if (activeScene.name == "Doodle")
            {
                record = PlayerPrefs.GetInt("DoodleRec");
                RecordText.GetComponent<Text>().text = "High Score: " + record;
            }
            if (activeScene.name == "Falling")
            {
                record = PlayerPrefs.GetInt("FallingRec");
                RecordText.GetComponent<Text>().text = "High Score: " + record;
            }
        }

    }

    private void FixedUpdate()
    {
        
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            State = States.Hero_Jump;
        }
    }

    private void Update()
    {
        if (isGrounded)
        {
            State = States.Hero_Idle;
        }
        if (Input.GetButton("Horizontal"))
        {
            Move();
        }
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump(JumpForse);
        }

    }

    public void Score(int summ)
    {
        score += summ;
        

        if (activeScene.name == "Doodle" && score > record)
        {
            PlayerPrefs.SetInt("DoodleRec", score);
            PlayerPrefs.Save();
            record = PlayerPrefs.GetInt("DoodleRec");
            RecordText.GetComponent<Text>().text = "High Score: " + record;
        }
        else if (activeScene.name == "Falling" && score > record)
        {
            PlayerPrefs.SetInt("FallingRec", score);
            PlayerPrefs.Save();
            record = PlayerPrefs.GetInt("FallingRec");
            RecordText.GetComponent<Text>().text = "High Score: " + record;
        }

        ScoreText.GetComponent<Text>().text = "Score: " + score;
    }

    private void OnDestroy()
    {
        if (live <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }


    private void Move()
    {
        if (isGrounded)
        {
            State = States.Hero_Run;
        }
        Vector3 move = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + move, speed * Time.deltaTime);
        sprite.flipX = move.x > 0.0f;
    }

    private void Jump(float JumpForse)
    {
        rigidbody.AddForce(transform.up * JumpForse, ForceMode2D.Impulse);
    }

    public void DooddleJump(float DooddleForse)
    {
        Jump(DooddleForse);
    }
}

public enum States
{
    Hero_Idle,
    Hero_Run,
    Hero_Jump
}


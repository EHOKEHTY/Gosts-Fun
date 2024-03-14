using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bonus;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float spawnDistance;
    private float biasXmin = -5.5f;
    private float biasXmax = 2.2f;
    private float StartPositionY;
    bool doodleMode = true;
    bool fallingMode = true;
    
    private void Start()
    {
        StartPositionY = transform.position.y;
    }

    private void Update()
    {
        if (StartPositionY > transform.position.y + 1 && fallingMode)
        {
            doodleMode = false;
            Spawn();
            BonusSpawn();
            StartPositionY = transform.position.y - spawnDistance;
        }
        if (StartPositionY < transform.position.y - 1 && doodleMode)
        {
            fallingMode = false;
            biasXmax = 7.2f;
            biasXmin = -6.7f;
            Spawn();
            StartPositionY = transform.position.y + spawnDistance + Random.Range(-2, 1);
        }
    }
    private void Spawn()
    {
        float biasX = Random.Range(biasXmin, biasXmax);
        Vector3 offset = new Vector3(transform.position.x + biasX, 0,0f);
        Instantiate(obstacle, transform.position + offset, transform.rotation);
    }
    private void BonusSpawn()
    {
        if (Random.Range(0, 6) == 0)
        {
            float randPos = Random.Range(-6f, 6.5f);
            Vector3 offset = new Vector3(transform.position.x + randPos, 1f, 0f);
            Instantiate(bonus[Random.Range(0, bonus.Length)], transform.position + offset, transform.rotation);
        }      
    }
}
using UnityEngine;

public class Cannon : Entity
{
    [SerializeField] Projectile projectile;
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    //private PlayerStatus status;
    //private Rigidbody2D rb;
    //private Vector2 obstaclePos;
    bool inRange = false;
    float timer = 1f;
    float time = 0f;
    void Start()
    {
        time = timer;
        player = GameObject.FindGameObjectWithTag("Player");
        //status = player.GetComponent<PlayerStatus>();
        //obstaclePos = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        DetectPlayer();
        if (!inRange)
            return;
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Projectile bulletObj = Instantiate(projectile, spawnPoint.position, Quaternion.identity);

            if (transform.localScale.x < 0f)
            {
                //bullet move to the left
                bulletObj.SetDirection(1);
            }
            else
            {
                //bullet move to the right
                bulletObj.SetDirection(-1);
            }
            time = timer;
        }
    }
    private void DetectPlayer()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= 10f)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
}

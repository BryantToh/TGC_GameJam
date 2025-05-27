using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;
    float timer = 2f;
    float time = 0f;
    void Start()
    {
        time = timer;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Instantiate(projectile, spawnPoint.position, Quaternion.identity);
            time = timer;
        }
    }
}

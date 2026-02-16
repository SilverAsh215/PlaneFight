using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRate = 1;

    private float _timer = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1 / spawnRate)
        {
            _timer -= 1 / spawnRate;
            SpawnBullet();    
        }
    }

    void SpawnBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}

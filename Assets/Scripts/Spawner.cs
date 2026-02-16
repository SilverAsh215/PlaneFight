using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject intervalPrefab;
    public float spawnRate = 1.0f;
    public float widthRange = 2.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefab), 1, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnPrefab()
    {
        float x = Random.Range(-widthRange, widthRange);
        Instantiate(intervalPrefab,new Vector3(x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}

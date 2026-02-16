using UnityEngine;

public enum AwardType
{
    SuperGun,
    Bomb
}

public class Award : MonoBehaviour
{
    public float speed = 0.8f;
    
    public AwardType awardType;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.down);
        
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}

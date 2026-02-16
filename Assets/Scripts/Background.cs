using UnityEngine;

public class Background : MonoBehaviour
{

    public float speed = 2.0f;

    public Transform otherBg;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 使该背景向下运动
        transform.Translate(Time.deltaTime * speed * Vector3.down);
        
        // 如果该背景超出摄像机范围，则将该背景的位置置于该背景上面的背景的上面，如此交替移动，实现滚动的效果
        // 但由于脚本在两个background中运行，也就是在不同的Update中运行，会导致不同步的情况，导致产生缝隙
        if (transform.position.y < -8.52f)
            transform.position = new Vector3(otherBg.position.x, otherBg.position.y + 8.52f, otherBg.position.z);
    }
}

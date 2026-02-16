using UnityEngine;

public class BgController : MonoBehaviour
{
    // 速度
    public float speed = 2.0f;
    
    // 背景
    public Transform bg1;
    public Transform bg2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 使该背景向下运动
        bg1.Translate(speed * Time.deltaTime * Vector3.down);
        bg2.Translate(speed * Time.deltaTime * Vector3.down);
        
        // 如果该背景超出摄像机范围，则将该背景的位置置于该背景上面的背景的上面，如此交替移动，实现滚动的效果
        if (bg1.position.y < -8.52f)
            bg1.position = new Vector3(bg2.position.x, bg2.position.y + 8.52f, bg2.position.z);
        if (bg2.position.y < -8.52f)
            bg2.position = new Vector3(bg1.position.x, bg1.position.y + 8.52f, bg1.position.z);
    }
}

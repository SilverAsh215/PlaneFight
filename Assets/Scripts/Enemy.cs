using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;

    public int hp = 1;

    private bool _isDamage;
    private bool _isDeath;

    public Sprite[] damageSprites;
    public Sprite[] deathSprites;
    public float frameRate = 10.0f;
    private float _timer;
    private int _currentFrame;
    
    
    private SpriteRenderer _spriteRenderer;
    private Sprite _idleSprite;
    
    private Collider2D _collider;

    public int score = 100;

    public AudioSource deathAudio;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _idleSprite = _spriteRenderer.sprite;
        
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        
        if (_isDamage)
        {
            PlayAnimationUpdate(damageSprites);
        }
        else if (_isDeath)
        {
            PlayAnimationUpdate(deathSprites);
        }
    }

    void MoveUpdate()
    {
        if (!_isDeath)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        }
        if (transform.position.y < -6.0f)
        {
            Destroy(gameObject);
        }
    }

    void PlayAnimationUpdate(Sprite[] sprites)
    {
        _timer += Time.deltaTime;
        
        if (_timer > 1 / frameRate)
        {
            _timer -= 1 / frameRate;
            _currentFrame++;
        }
        if (_currentFrame >= sprites.Length)
        {
            if (_isDamage)
            {
                ResetState();
                _isDamage = false;
            }
            if (_isDeath)
            {
                _spriteRenderer.enabled = false;
                Destroy(gameObject, 5);
            }
        }
        else
        {
            _spriteRenderer.sprite = sprites[_currentFrame];
        }
    }

    void ResetState()
    {
        _timer = 0.0f;
        _currentFrame = 0;
        _spriteRenderer.sprite = _idleSprite;
    }

    void TakeDamage()
    {
        if (_isDeath) return;
            
        hp--;
        
        if (hp <= 0)
        {
            Die();
        }
        else
        {
            ResetState();
            _isDamage = true;
        }
    }

    public void KillEnemy()
    {
        Die();
    }

    void Die()
    {
        ResetState();
        _isDeath = true;
        _collider.enabled = false;
        GameManager.Instance.AddScore(score);
        deathAudio.Play();
    }
}

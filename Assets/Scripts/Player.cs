using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Camera _camera;
    
    public int frameRate = 10;
    public Sprite[] idleSprite;
    public Sprite[] deathSprite;
    
    private SpriteRenderer _spriteRenderer;
    private float _timer;
    private int _currentFrame;

    private Vector3 _lastMousePosition = Vector3.zero;
    private bool _isMouseDown;

    public float superGunDuration = 3.0f;
    private float _superGunTimer;

    public GameObject gunTop;
    public GameObject gunLeft;
    public GameObject gunRight;

    public int hp = 5;
    public float invincibleTime = 2.0f;
    private bool _isInvincible;
    private float _invincibleTimer;
    
    public float blinkInterval = 0.1f;

    public AudioSource getBombAudio;
    public AudioSource getSuperGunAudio;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        IdleAnimationUpdate();
        
        DeathAnimationUpdate();
        
        MoveUpdate();
        
        SuperGunUpdate();
    }

    void IdleAnimationUpdate()
    {
        if (hp <= 0) return;
        
        _timer += Time.deltaTime;
        if (_timer >= 1f / frameRate)
        {
            _timer-= 1f / frameRate;
            _currentFrame = (_currentFrame + 1) % idleSprite.Length;
            _spriteRenderer.sprite = idleSprite[_currentFrame];
        }
    }

    void DeathAnimationUpdate()
    {
        if (hp > 0) return;
        
        _timer += Time.deltaTime;
        if (_timer >= 1f / frameRate)
        {
            _timer-= 1f / frameRate;
            _currentFrame++;
        } 
        if (_currentFrame >= deathSprite.Length)
        {
            enabled = false;
            GameManager.Instance.GameOver();
        }
        else
        {
            _spriteRenderer.sprite = deathSprite[_currentFrame];
        }
    }

    void MoveUpdate()
    {
        if (GameManager.Instance.IsPause()) return;
        
        var mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            _isMouseDown = true;
            _lastMousePosition = _camera.ScreenToWorldPoint(mouse.position.ReadValue());
        }

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            _isMouseDown = false;
        }
        
        if (_isMouseDown)
        {
            Vector3 currentPosition = _camera.ScreenToWorldPoint(mouse.position.ReadValue());
            Vector3 offset = currentPosition - _lastMousePosition;
            transform.position += offset;
            CheckPosition();
            _lastMousePosition = currentPosition;
        }
    }

    private void CheckPosition()
    {
        Vector3 position = transform.position;
        if (position.x < -2.2f)
        {
            position.x = -2.2f;
        } 
        if (position.x > 2.2f)
        {
            position.x = 2.2f;
        }
        if (position.y < -3.8f)
        {
            position.y = -3.8f;
        }
        if (position.y > 3.8f)
        {
            position.y = 3.8f;
        }
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Award"))
        {
            if (collision.GetComponent<Award>().awardType == AwardType.SuperGun)
            {
                getSuperGunAudio.Play();
                TransformToSuperGun();
            }
            else
            {
                getBombAudio.Play();
                GameManager.Instance.AddBomb();
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy") && !_isInvincible)
        {
            collision.gameObject.SendMessage("TakeDamage");
            hp--;
            if (hp <= 0)
            {
                TransformToDeath();
            }
            else
            {
                TransformToInvincible();
            }
        }
    }
    
    void TransformToDeath()
    {
        _timer = 0.0f;
        _currentFrame = 0;
        DisableAllGuns();
    }
    
    void SuperGunUpdate()
    {
        if (_superGunTimer > 0)
        {
            _superGunTimer -= Time.deltaTime;
            
            if (_superGunTimer <= 0)
            {
                TransformToNormalGun();
            }
        }
    }
    
    void TransformToSuperGun()
    { 
        gunLeft.SetActive(true);
        gunRight.SetActive(true);
        gunTop.SetActive(false);
        
        _superGunTimer = superGunDuration;
    }
    
    void TransformToNormalGun()
    {
        gunLeft.SetActive(false);
        gunRight.SetActive(false);
        gunTop.SetActive(true);
    }
    
    void DisableAllGuns()
    {
        gunLeft.SetActive(false);
        gunRight.SetActive(false);
        gunTop.SetActive(false);
    }

    void TransformToInvincible()
    {
        _isInvincible = true;
        _invincibleTimer = 0.0f;
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        while (_invincibleTimer <= invincibleTime)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            _invincibleTimer += blinkInterval;
        }
        
        _spriteRenderer.enabled = true;
        _isInvincible = false;
    }
}

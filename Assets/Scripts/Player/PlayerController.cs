using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private Animator _animator;
    
    public float moveSpeed = 8f;
    public float jumpSpeed = 7f;
    public float climbSpeed = 5f;

    public bool playerIsGrounded;
    public bool playerIsWalled;
    
    public Transform groundCheck;
    public Transform wallCheck;
    
    public LayerMask whatIsGround;
    

    
    public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);
    public Vector2 wallBoxingSize = new Vector2(0.2f, 0.4f);

    public int playerHealth;
    public float damageColdown;
    private float _damageColdownTimer;

    [Header("Audio")] 
    public AudioClip[] climbSounds;
    public AudioClip[] jumpSounds;
    
    
    private void Start()
    {
        _input = GetComponentInParent<InputActions>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        playerIsGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, whatIsGround);
        
        playerIsWalled = Physics2D.OverlapBox(wallCheck.position, wallBoxingSize, 0f, whatIsGround);
        
        //Jump if Grounded
        if (_input.Jump && playerIsGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
            
            _audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
        }
        
        //Climbing
        if (playerIsWalled && _input.Vertical > 0f)
        {
            _rigidbody2D.linearVelocityY = climbSpeed;
            _rigidbody2D.gravityScale = 0f;
        }
        
        //Stuck to wall
        else if (playerIsWalled == true)
        {
            _rigidbody2D.linearVelocityY = 0f;
            _rigidbody2D.gravityScale = 0f;
        }
        
        //Normal falling
        else
        {
            _rigidbody2D.gravityScale = 1f;
        }
        UpdateAnimation();
        Attack();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
        
        //Left or Right
        if (_input.Horizontal != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(_input.Horizontal), 1f);
        }
    }
    private void UpdateAnimation()
    {
        if (playerIsGrounded)
        {
            if (_input.Horizontal != 0 && !playerIsWalled)
            {
                _animator.Play("Player_Walk");
            }
            else
            {
                _animator.Play("Player_Idle");
            }
        }
        else
        {
            if (_rigidbody2D.linearVelocityY > 0)
            {
                _animator.Play("Player_Jump");
            }
            else
            {
                _animator.Play("Player_Fall");
            }
        }
    }
    private void Attack()
    {
        //Sjekk at vi ikke koliderer med en fiende
        if (!Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Enemy"))) return;

        //Sjekk om fienden colliderer med groundCollider
        var enemyCollider = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, LayerMask.GetMask("Enemy"));
        
        //foreach går gjennom hvert element i listen
        foreach (var enemy in enemyCollider)
        {
            Destroy(enemy.gameObject);
        }
        
        //Jump-bust etter treff
        _rigidbody2D.linearVelocityY = jumpSpeed/1.3f;
    }
    private void OnDrawGizmos()
    {
        //groundCollider
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        
        //wallCollider
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(wallCheck.position, wallBoxingSize);
    }
    
    private static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Death"))
        {
            RestartScene();
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
    
    private void TakeDamage()
    {
        if (Time.time > _damageColdownTimer)
        {
            playerHealth -= 1;
            _damageColdownTimer = Time.time + damageColdown;
            print("Healt is " + playerHealth);
        }

        if (playerHealth <= 0)
        {
            RestartScene();
        }
    }
}

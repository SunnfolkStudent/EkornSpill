using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;

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
    
    private void Start()
    {
        _input = GetComponentInParent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerIsGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, whatIsGround);
        
        playerIsWalled = Physics2D.OverlapBox(wallCheck.position, wallBoxingSize, 0f, whatIsGround);
        
        //Jump if Grounded
        if (_input.Jump && playerIsGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }
        
        if (playerIsWalled && _input.Vertical > 0f)
        {
            _rigidbody2D.linearVelocityY = climbSpeed;
            _rigidbody2D.gravityScale = 0f;
            Debug.Log("Climbing");
        }
        
        else if (playerIsWalled == true)
        {
            _rigidbody2D.linearVelocityY = 0f;
            _rigidbody2D.gravityScale = 0f;
            Debug.Log("Player is walled");
        }
        
        else
        {
            _rigidbody2D.gravityScale = 1f;
        }
        
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
        
        //If moving left or right
        if (_input.Horizontal <= -1)
        {
            //Turn Right
            if (transform.localScale.x <= 0) return;
            transform.localScale = new Vector2(transform.localScale.x * -1f, 1f);
            Debug.Log("Right");
        }
        else
        {
            //Turn Left
            if (transform.localScale.x >= 0) return;
            transform.localScale = new Vector2(transform.localScale.x * -1f, 1f);
            Debug.Log("Left");
        }
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

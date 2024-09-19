using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;

    public float moveSpeed = 8f;
    public float jumpSpeed = 7f;

    public bool playerIsGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);

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
        
        if (_input.Jump && playerIsGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
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

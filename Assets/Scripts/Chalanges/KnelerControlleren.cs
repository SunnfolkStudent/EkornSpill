using UnityEngine;

public class KnelerControlleren : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    
    public float sightRange;
    public bool canJump;
    public bool enemyIsOnGround;

    public Transform wallCheck;
    public Transform groundCheck;

    public LayerMask whatIsGround;
    public LayerMask whatIsBounceable;
   
    private float timeCounter;
    private float timeCountValue = 2f;
    private Transform target;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }
    
    private void Update()
    {
        enemyIsOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        
        
        if (target != null)
        {
            _moveDirection = target.position - transform.position;
            
            if (enemyIsOnGround && transform.position.x > target.position.x)
            {
                transform.localScale = transform.position.x > target.position.x ? 
                    new Vector2(1, 1) : new Vector2(-1, 1);
            }
        
            if (enemyIsOnGround && Vector2.Distance(target.position, transform.position) < sightRange)
            {
                canJump = true;
            }
        }
        
        if (canJump && !Physics2D.OverlapCircle(wallCheck.position, 0.2f, whatIsGround))
        {
            if (Time.time > timeCounter)
            {
                Jump();
                timeCounter = Time.time + timeCountValue;
            }
        }

      /*  if (Physics2D.OverlapBox(wallCheck.position, new Vector2(0.2f, 0.5f), 0, whatIsBounceable))
        {
            Bounce();
        }*/
    }

    private void Jump()
    {
        _rigidbody2D.linearVelocityX = _moveDirection.x * moveSpeed;
        _rigidbody2D.linearVelocityY = jumpSpeed;
        canJump = false;
    }

    private void Bounce()
    {
        _rigidbody2D.linearVelocityY = jumpSpeed/1.3f;
        _rigidbody2D.linearVelocityX = (moveSpeed/1.3f) * transform.localScale.x;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(wallCheck.position, new Vector2(0.2f, 0.5f));
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        Gizmos.color = Color.cyan;                                
        Gizmos.DrawWireSphere(transform.position, sightRange);    
    }
}

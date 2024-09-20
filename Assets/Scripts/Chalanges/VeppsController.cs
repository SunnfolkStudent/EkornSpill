using UnityEngine;

public class VeppsController : MonoBehaviour
{
    public float moveSpeed;
    public float sightRange;

    public LayerMask whatIsGround;
    
    private Animator _animator;
    
    public bool _canAttack;
    public bool dying;
    private Vector3 _attackDirection;
    private Transform _target;

    private void Start()
    {
        _target = GameObject.Find("Player").transform;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (dying) return;
        if (!Physics2D.OverlapCircle(transform.position, 0.2f, whatIsGround))
        {
            
            if(_canAttack == false && Vector2.Distance(_target.position, transform.position) < sightRange)
            {
                _attackDirection = Vector3.Normalize(_target.position - transform.position);
                _canAttack = true;
                
            }
        }

        if (_canAttack == true)
        {
            transform.position += _attackDirection * (moveSpeed * Time.deltaTime);

            transform.localScale = new Vector2(Mathf.Sign(_attackDirection.x), 1f);
            
            _animator.Play("Veps_Attack");
            
            if(Physics2D.OverlapCircle(transform.position, 0.5f, whatIsGround))
            {
                _canAttack = false;
                dying = true;
                Destroy(gameObject, 0.5f);
                
            }
        }
        else
        {
            _animator.Play("Veps_Idle");
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;                               
        Gizmos.DrawWireSphere(transform.position, 0.5f);   
        
    }
}

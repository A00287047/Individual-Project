using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
 

    public KeyCode meleeAttackKey = KeyCode.Mouse0;
    public KeyCode jumpKey = KeyCode.Space;
    public string xMoveAxis = "Horizontal";

    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;

    private Rigidbody2D rb2D = null;
    private Animator animator = null;
    private float moveIntentionX = 0;
    private bool attemptJump = false;
    private bool attemptMeleeAttack = false;
    private float timeUntilMeleeReaded = 0;
    private bool isMeleeAttacking = false;

    public Transform meleeAttackOrigin = null;
    public float meleeAttackRadius = 0.6f;
    public float meleeDamage = 2f;
    public float meleeAttackDelay;
    public LayerMask enemyLayer = 8;

    public Animator Animator
    {
        get { return animator; }
        protected set { animator = value; }
    }

    void Start()
    {
        if (GetComponent<Rigidbody2D>())
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        GetInput();
        HandleJump();
        HandleAttack();
        HandleAnimations();
    }

    void FixedUpdate()

    {
        HandleRun();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
            SoundManager.sndMan.PlayCoinSound();
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, -Vector2.up * groundedLeeway, Color.green);
        if (meleeAttackOrigin != null)
        {
            Gizmos.DrawWireSphere(meleeAttackOrigin.position, meleeAttackRadius);
        }
    }


    private void GetInput()
    {
        moveIntentionX = Input.GetAxis(xMoveAxis);
        attemptMeleeAttack = Input.GetKeyDown(meleeAttackKey);
        attemptJump = Input.GetKeyDown(jumpKey);
    }

private void HandleAnimations()
    {
        Animator.SetBool("Grounded", CheckGrounded());
        if (attemptMeleeAttack)
        {
            if(!isMeleeAttacking)
            {
                StartCoroutine(MeleeAttackAnimDelay());
            }
        }
        if (attemptJump && CheckGrounded() || rb2D.velocity.y > 1f)
        {
            if(!isMeleeAttacking)
            {
                Animator.SetTrigger("Jump");
            }
        }
        if(Mathf.Abs(moveIntentionX) > 0.1f && CheckGrounded())
        {
            Animator.SetInteger("AnimState", 2);
        }
        else
        {
            Animator.SetInteger("AnimState", 0);
        }

    }

    private IEnumerator MeleeAttackAnimDelay()
    {
        Animator.SetTrigger("Attack");
        isMeleeAttacking = true;
        yield return new WaitForSeconds(meleeAttackDelay);
        isMeleeAttacking = false;

    }

private void HandleRun()

    {
        if (moveIntentionX > 0 && transform.rotation.y == 0 && !isMeleeAttacking)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (moveIntentionX < 0 && transform.rotation.y != 0 && !isMeleeAttacking)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rb2D.velocity = new Vector2(moveIntentionX * speed, rb2D.velocity.y);

    }


    private void HandleJump()
    {
        if (attemptJump && CheckGrounded())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }

    }

    private void HandleAttack()
    {
        if (attemptMeleeAttack && timeUntilMeleeReaded <= 0)
        {
            Debug.Log("Player: Attempting Attack");
            Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(meleeAttackOrigin.position, meleeAttackRadius, enemyLayer);
            for(int i=0; i < overlappedColliders.Length; i++)
            {
                Damage enemyAttributes = overlappedColliders[i].GetComponent<Damage>();
                if(enemyAttributes != null)
                {
                    enemyAttributes.ApplyDamage(meleeDamage);
                }
            }
            timeUntilMeleeReaded = meleeAttackDelay;
        }
        else
        {
            timeUntilMeleeReaded -= Time.deltaTime;
        }

    }


    private bool CheckGrounded()

    {
        return Physics2D.Raycast(transform.position, -Vector2.up, groundedLeeway);
    }
}


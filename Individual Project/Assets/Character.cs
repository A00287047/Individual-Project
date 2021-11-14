using UnityEngine;

public class Character : MonoBehaviour
{
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode jumpKey = KeyCode.Space;
    public string xMoveAxis = "Horizontal";
    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;
    private Rigidbody2D rb2D = null;
    private float moveIntentionX = 0;
    private bool attemptJump = false;
    private bool attemptAttack = false;

    public Transform attackOrigin = null;
    public float attackRadius = 0.6f;
    public float meleeDamage = 2f;
    public float attackDelay;
    public LayerMask enemyLayer = 8;
    private float timeUntilMeleeReaded;

    void Start()
    {
        if (GetComponent<Rigidbody2D>())

        {

            rb2D = GetComponent<Rigidbody2D>();
        }

    }

    void Update()

    {

        GetInput();


        HandleJump();


        HandleAttack();


    }

    void FixedUpdate()

    {

        HandleRun();

    }


    private void OnDrawGizmosSelected()

    {

        Debug.DrawRay(transform.position, -Vector2.up * groundedLeeway, Color.green);
        if(attackOrigin != null)
        {
            Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
        }
    }


    private void GetInput()

    {


        moveIntentionX = Input.GetAxis(xMoveAxis);

        attemptAttack = Input.GetKeyDown(attackKey);

        attemptJump = Input.GetKeyDown(jumpKey);

    }

    private void HandleRun()

    {

        if (moveIntentionX > 0 && transform.rotation.y == 0)

        {

            transform.rotation = Quaternion.Euler(0, 180f, 0);


        }

        else if (moveIntentionX < 0 && transform.rotation.y != 0)

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
        if (attemptAttack && timeUntilMeleeReaded <= 0)
        {
            Debug.Log("Player: Attempting Attack");
            Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyLayer);
            for(int i=0; i < overlappedColliders.Length; i++)
            {
                Damage enemyAttributes = overlappedColliders[i].GetComponent<Damage>();
                if(enemyAttributes != null)
                {
                    enemyAttributes.ApplyDamage(meleeDamage);
                }
            }
            timeUntilMeleeReaded = attackDelay;
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


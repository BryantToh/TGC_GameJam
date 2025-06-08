using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    protected enum STATE
    {
        IDLE,
        CHASE,
        ATTACK
    }

    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float damage = 4f;
    [SerializeField] protected float movementSpeed = 2f;
    [SerializeField] protected float attackRange;
    [SerializeField] private float attackTime;
    [SerializeField] protected bool canReturnToIdle = false;
    protected Animator anim;
    private GameObject player;
    private PlayerStatus status;

    protected STATE currentState = STATE.IDLE;
    private float attackTimer;
    private bool isFacingRight = true;

    protected virtual void Start()
    {
        isFacingRight = true;

        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        status = player.GetComponent<PlayerStatus>();
    }

    protected virtual void Update()
    {
        if (player == null)
        {
            Debug.Log("Missing player");
            return;
        }

        switch (currentState)
        {
            case STATE.IDLE:
                IdleState();
                if (TargetInAttackRange())
                {
                    ChangeState(STATE.ATTACK);
                }
                else if (TargetInRange())
                {
                    ChangeState(STATE.CHASE);
                }
                break;

            case STATE.CHASE:
                ChaseState();
                if (TargetInAttackRange())
                {
                    ChangeState(STATE.ATTACK);
                }
                else if (canReturnToIdle && !TargetInRange())
                {
                    ChangeState(STATE.IDLE);
                }
                break;

            case STATE.ATTACK:
                AttackState();
                if (!TargetInAttackRange())
                {
                    ChangeState(STATE.CHASE);
                }
                break;
        }
    }



    protected virtual void IdleState()
    {
        //Debug.Log("Current state is IDLE");
    }

    protected virtual void ChaseState()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * movementSpeed);

            if (player.transform.position.x < transform.position.x && !isFacingRight)
            {
                FlipX();
            }
            else if (player.transform.position.x > transform.position.x && isFacingRight)
            {
                FlipX();
            }
        }
    }
    protected virtual void AttackState()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            anim.SetTrigger("canAttack");
            attackTimer = attackTime;
        }
    }
    protected virtual bool TargetInRange()
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) < chaseRange;
    }

    protected virtual bool TargetInAttackRange()
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) <= attackRange;
    }

    protected void ChangeState(STATE newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    protected void FlipX()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void DealDamageToPlayer()
    {
        if (TargetInAttackRange())
        {
            status.TakeDamage(damage);
        }
        else
        {
            Debug.Log("animal missed ur ass");
        }
    }
}

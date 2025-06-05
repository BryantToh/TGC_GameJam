using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    protected enum STATE
    {
        IDLE,
        CHASE
    }

    [SerializeField] private float chaseRange = 5f;
    [SerializeField] protected float movementSpeed = 2f;
    [SerializeField] protected bool canReturnToIdle = false;
    protected Animator anim;
    private GameObject player;

    protected STATE currentState = STATE.IDLE;
    private bool isFacingRight = true;

    protected virtual void Start()
    {
        isFacingRight = true;

        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        if (player == null)
        {
            Debug.Log("Missing player");
        }
        switch (currentState)
        {
            case STATE.IDLE:
                IdleState();
                if (TargetInRange())
                {
                    ChangeState(STATE.CHASE);
                }
                break;
            case STATE.CHASE:
                ChaseState();
                if (canReturnToIdle && !TargetInRange())
                {
                    ChangeState(STATE.IDLE);
                    Debug.Log("State Chase");
                }
                break;
        }
    }

    protected virtual void IdleState()
    {
        Debug.Log("Current state is IDLE");
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

    protected virtual bool TargetInRange()
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) < chaseRange;
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
}

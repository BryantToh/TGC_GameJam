using UnityEngine;

public class Dog : Animal
{
    private void Awake()
    {
        canReturnToIdle = true;
    }

    protected override void IdleState()
    {
        anim.SetBool("canChase", false);
        base.IdleState();
    }

    protected override void ChaseState()
    {
        anim.SetBool("canChase", true);
        base.ChaseState();
    }
    protected override void AttackState()
    {
        base.AttackState();
    }
}

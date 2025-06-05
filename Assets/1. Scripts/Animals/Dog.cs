using UnityEngine;

public class Dog : Animal
{
    private void Awake()
    {
        canReturnToIdle = true;
    }

    protected override void IdleState()
    {
        base.IdleState();
    }

    protected override void ChaseState()
    {
        base.ChaseState();
    }
}

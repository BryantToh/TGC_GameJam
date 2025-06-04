using UnityEngine;

public class Boar : Animal
{
    private void Awake()
    {
        canReturnToIdle = false;
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

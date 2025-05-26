using UnityEngine;

public class TestEntity : Entity, IInteractable
{
    // Add effects or something here
    public void Interact()
    {
        Debug.Log("Interacting with Test Object");
    }
}

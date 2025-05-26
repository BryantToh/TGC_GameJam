using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "M_", menuName = "ScriptableObjects/Memory")]
public class MemorySO : ScriptableObject
{
    public List<GameObject> listOfMemories;
    public bool[] numOfKeys = new bool[4];

    public void CollectKey(int index)
    {
        numOfKeys[index] = true;
    }
}

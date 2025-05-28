using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "M_", menuName = "ScriptableObjects/Memory")]
public class MemorySO : ScriptableObject
{
    public List<GameObject> listOfMemories;
    public bool[] numOfKeys = new bool[4];
    public void Reset()
    {
        for (int i = 0; i < numOfKeys.Length; i++)
        {
            numOfKeys[i] = false;
        }
    }
    public void CollectKey(int index)
    {
        numOfKeys[index] = true;
    }
}

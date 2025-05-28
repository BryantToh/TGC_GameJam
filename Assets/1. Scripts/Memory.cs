using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] MemorySO memorySO;
    public List<GameObject> memoryPanel = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < memoryPanel.Count; i++)
        {
            memoryPanel[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (memorySO != null)
        {
            for (int i = 0; i < memorySO.numOfKeys.Length; i++)
            {
                if (memorySO.numOfKeys[i])
                {
                    memoryPanel[i].SetActive(true);
                }
                else
                {
                    memoryPanel[i].SetActive(false);
                }
            }
        }
    }
}

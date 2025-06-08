using System.Collections;
using UnityEngine;

public class GrandpaInteraction : MonoBehaviour, IInteractable
{
    public bool isStanding = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("IsStanding", isStanding);     
    }
    public void Interact()
    {
        if (LevelManager.Instance.IsLastLevel())
        {
            StartCoroutine(HandleEndGame());
        }
        else
        {
            LevelManager.Instance.SetCompleteLevel(true);
        }
    }

    private IEnumerator HandleEndGame()
    {
        LevelManager.Instance.SetCompleteLevel(true);
        yield return ScreenFade.Instance.FadeToBlack();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }

        MemoriesManager.Instance.StartMemorySequence();
    }
}

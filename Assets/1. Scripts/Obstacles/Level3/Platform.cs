using UnityEngine;
using DG.Tweening;
using System;

public class Platform : MonoBehaviour
{
    public enum PLATFORM_TYPE
    {
        MOVE,
        DROP,
        UP,
        NONE
    }

    public PLATFORM_TYPE platformType = PLATFORM_TYPE.MOVE;

    [Header("Movements")]
    [SerializeField] private float moveDist = 2f;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Drop Settings")]
    [SerializeField] private float dropDelay = 0.2f;
    [SerializeField] private float dropSpeed = 2f;
    [SerializeField] private float dropDist = 2f;
    [SerializeField] private float disableGODelay = 0.5f;

    private Vector2 originalPos;
    private bool hasMoved = false;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMoved) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (platformType)
            {
                case PLATFORM_TYPE.MOVE:
                    MoveRandom();
                    break;
                case PLATFORM_TYPE.DROP:
                    DropPlatform();
                    break;
                case PLATFORM_TYPE.UP:
                    MoveUp();
                    break;
                case PLATFORM_TYPE.NONE:
                    break;
            }
        }
    }

    private void MoveRandom()
    {
        hasMoved = true;

        int direction = UnityEngine.Random.value > 0.5f ? 1 : -1;
        Vector2 targetPos = originalPos + Vector2.up * direction * moveDist;
        float duration = moveDist / moveSpeed;

        transform.DOMove(targetPos, duration)
            .SetEase(Ease.InOutSine);
    }

    private void MoveUp()
    {
        hasMoved = true;

        Vector2 targetPos = originalPos + Vector2.up * moveDist;
        float duration = moveDist / moveSpeed;

        transform.DOMove(targetPos, duration)
            .SetEase(Ease.InOutSine);
    }

    private void DropPlatform()
    {
        hasMoved = true;

        DOVirtual.DelayedCall(dropDelay, () =>
        {
            Vector2 targetPos = originalPos + Vector2.down * dropDist;
            float duration = dropDist / dropSpeed;

            transform.DOMove(targetPos, duration)
                .SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(disableGODelay, () =>
                    {
                        gameObject.SetActive(false);
                    });
                });
        });
    }
}


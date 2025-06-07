using UnityEngine;
using DG.Tweening;

public abstract class UIShakeEffect : MonoBehaviour
{
    public void ShakeUI(RectTransform rectTransform, float duration, float strength)
    {
        rectTransform.DOShakeAnchorPos(duration, strength);
    }
}

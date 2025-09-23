using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText;

    [Header("Settings")]
    [SerializeField] private Color _eggCounterColor;
    [SerializeField] private float _colorChangeDuration;
    [SerializeField] private float _scaleChangeDuration;

    private RectTransform _eggCounterTransform;

    void Awake()
    {
        _eggCounterTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();
    }

    public void SetEggCounterText(int counter, int max)
    {
        _eggCounterText.text = counter.ToString() + "/" + max.ToString();
    }

    public void SetEggCompleted()
    {
        _eggCounterText.DOColor(_eggCounterColor, _colorChangeDuration);
        _eggCounterTransform.DOScale(1.2f, _scaleChangeDuration).SetEase(Ease.OutBack);
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _healthImages;


    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthSprite;
    [SerializeField] private Sprite _playerUnhealthSprite;

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealtTransforms;

    private void Awake()
    {
        _playerHealtTransforms = new RectTransform[_healthImages.Length];

        for (int i = 0; i < _healthImages.Length; i++)
        {
            _playerHealtTransforms[i] = _healthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

        // FOR TESTING
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) AnimateDamage(1);
        if (Input.GetKeyDown(KeyCode.P)) AnimateDamage(3);
    }

    public void AnimateDamage(int damage = 1)
    {
        int getDamage = 0;
        for (int i = 0; i < _healthImages.Length; i++)
        {
            if (_healthImages[i].sprite == _playerHealthSprite)
            {
                AnimateDamageSprite(_healthImages[i], _playerHealtTransforms[i]);
                getDamage++;

                if (getDamage == damage) break;
            }
        }
    }

    private void AnimateDamageSprite(Image activeImage, RectTransform activeTransform)
    {
        activeTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnhealthSprite;
            activeTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}

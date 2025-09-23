using UnityEngine;

public class HealtManager : MonoBehaviour
{
    [SerializeField] private int _maxHealt = 3;
    private int _currentHealt;

    private void Start()
    {
        _currentHealt = _maxHealt;
    }


    public void Damage(int amount)
    {
        if (_currentHealt > 0)
        {
            _currentHealt -= amount;
            //UI ANIMATE DAMAGE

            if (_currentHealt <= 0)
            {
                // DEAD
            }
        }
    }

    public void Heal(int amount)
    {
        if (_currentHealt < _maxHealt)
        {
            _currentHealt = Mathf.Min(_currentHealt + amount, _maxHealt);
        }
    }
}

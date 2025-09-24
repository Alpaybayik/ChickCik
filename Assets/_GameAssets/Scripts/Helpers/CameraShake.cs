using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake Instance { get; private set; }

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;


    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingInstensity;


    private void Awake()
    {
        Instance = this;
        _cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f) {
                _cinemachineBasicMultiChannelPerlin.AmplitudeGain = Mathf.Lerp(_startingInstensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
            }
        }
    }


    private IEnumerator CameraShakeCoroutine(float intensity, float time, float delay)
    {
        yield return new WaitForSeconds(delay);

        _cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        _shakeTimer = time;
        _shakeTimerTotal = time;
        _startingInstensity = intensity;
    }


    public void ShakeCamera(float intensity, float time, float delay)
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, delay));
    }
}

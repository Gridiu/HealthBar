using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthChanging : MonoBehaviour
{
    private Slider _slider;
    private float _healthChange = 10f;

    private Coroutine _coroutine;
    
    private void Start()
    {   
        _slider = GetComponent<Slider>();
    }

    public void TryIncreaseHealth()
    {
        if (_slider.value == _slider.maxValue)
        {
            Debug.Log("Health can't be greater than 100.");
        }
        else
        {
            StopCurrentCoroutine();

            _coroutine = StartCoroutine(ChangeHealth(_slider.value + _healthChange > _slider.maxValue ? _slider.maxValue : _slider.value + _healthChange));
        }
    }

    public void TryDecreaseHealth()
    {
        if (_slider.value == _slider.minValue)
        {
            Debug.Log("Health can't be less than 0.");
        }
        else
        {
            StopCurrentCoroutine();

            _coroutine = StartCoroutine(ChangeHealth(_slider.value - _healthChange < _slider.minValue ? _slider.minValue : _slider.value - _healthChange));
        }
    }

    private void StopCurrentCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

            _coroutine = null;
        }
    }

    private IEnumerator ChangeHealth(float finishValue)
    {
        float changeStep = 10f;

        while (_slider.value != finishValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, finishValue, changeStep * Time.deltaTime);

            yield return null;
        }
    }
}

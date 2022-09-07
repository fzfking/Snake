using System;
using System.Collections;
using System.Collections.Generic;
using Sources.Architecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MapSizeSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ValueView;
    private Slider _slider;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(UpdateValue);
    }

    private void UpdateValue(float value)
    {
        ValueView.text = value.ToString("###.##");
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(UpdateValue);
    }

    public void SaveSize()
    {
        var newSize = MapSize.CreateNew((int)_slider.value);
        MapSize.Save(newSize);
    }
}

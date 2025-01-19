using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Camera actualCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        canvas = gameObject.GetComponentInParent<Canvas>();
        slider = GetComponent<Slider>();
        target = transform.parent;
        actualCamera = Camera.main;
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {

        if (!canvas.isActiveAndEnabled) canvas.enabled = true;

        slider.value = currentValue / maxValue;
    }

    private void Update()
    {
        transform.rotation = actualCamera.transform.rotation;
        transform.position = target.position + offset;
    }
}



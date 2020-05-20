using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = transform.parent.transform.parent.GetComponent<Fort>().health;
    }

    private void Update() {
        slider.value = transform.parent.transform.parent.GetComponent<Fort>().health;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}

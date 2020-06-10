using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;

    [SerializeField]
    private bool isBoat;
    [SerializeField]
    private bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if(isBoat){
            slider.maxValue = transform.parent.transform.parent.GetComponent<Boat>().health;
        }else if(isPlayer){
            slider.maxValue = transform.parent.transform.parent.GetComponent<Player>().health;
        }else{
            slider.maxValue = transform.parent.transform.parent.GetComponent<Fort>().health;
        }
        
    }

    private void Update() {

        if(isBoat){
            slider.value = transform.parent.transform.parent.GetComponent<Boat>().health;
        }else if(isPlayer){
            slider.value = transform.parent.transform.parent.GetComponent<Player>().health;
        }else{
            slider.value = transform.parent.transform.parent.GetComponent<Fort>().health;
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelAttribute : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textName;
    [SerializeField]
    private TextMeshProUGUI textValue;
    [SerializeField]
    private TextMeshProUGUI textMin;
    [SerializeField]
    private TextMeshProUGUI textMax;
    [SerializeField]
    private Slider slider;

    public void setPanel(string s, int value, int min, int max)
    {
        textName.text = s;
        textValue.text = value + "";
        textMin.text = min + "";
        textMax.text = max + "";
        slider.minValue = min;
        slider.maxValue = max;
        slider.value = value;
    }
    public void setActive(bool b)
    {
        gameObject.SetActive(b);
    }

    void Start()
    {
        
    }



    void Update()
    {
        
    }
}

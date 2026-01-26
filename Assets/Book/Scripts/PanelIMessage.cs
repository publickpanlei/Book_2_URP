using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PanelIMessage : MonoBehaviour
{
    [SerializeField]
    private int type;//0 个人 1 家族
    [SerializeField]
    private PanelAttribute[] panelAttribute;


    private void OnEnable()
    {
        if (type == 0)
        {
            for (int i = 0; i < panelAttribute.Length; i++)
            {
                if (i < PlayerData.instance.attributeOne.Count)
                {
                    panelAttribute[i].setPanel(PlayerData.instance.attributeOne[i].aName, PlayerData.instance.attributeOne[i].value, 
                        PlayerData.instance.attributeOne[i].min, PlayerData.instance.attributeOne[i].max);
                    panelAttribute[i].setActive(true);
                }
                else
                {
                    panelAttribute[i].setActive(false);
                }
            }
        }
        else if (type == 1)
        {
            for (int i = 0; i < panelAttribute.Length; i++)
            {
                if (i < PlayerData.instance.attributeFamily.Count)
                {
                    panelAttribute[i].setPanel(PlayerData.instance.attributeFamily[i].aName,PlayerData.instance.attributeFamily[i].value,
                        PlayerData.instance.attributeFamily[i].min, PlayerData.instance.attributeFamily[i].max);
                    panelAttribute[i].setActive(true);
                }
                else
                {
                    panelAttribute[i].setActive(false);
                }
            }
        }



    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}

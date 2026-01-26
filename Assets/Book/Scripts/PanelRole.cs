using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelRole : MonoBehaviour
{

    [SerializeField]
    private Image face;
    [SerializeField]
    private TextMeshProUGUI textName;
    [SerializeField]
    private TextMeshProUGUI textValue0;
    [SerializeField]
    private TextMeshProUGUI textValue1;
    [SerializeField]
    private TextMeshProUGUI textBuff;

    public void initThis(string role, string job, int year, int v0, int v1, List<string> buff)
    {
        setRoleImage(role);
        textName.text = role + "(" + year + ")" + job;
        textValue0.text = v0 + "";
        textValue1.text = v1 + "";
        for (int i = 0; i < buff.Count; i++)
        {
            textBuff.text += (buff[i] + " ");
        }
    }
    private void setRoleImage(string s)
    {
        Sprite sprite = Resources.Load<Sprite>("Roles/" + s);
        if (sprite != null)
        {
            face.sprite = sprite;
        }
    }
    void Start()
    {
        
    }

    

    void Update()
    {
        
    }
}

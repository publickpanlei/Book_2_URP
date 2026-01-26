using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageSelectNeed : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] text;
    private string[] txts = { "", "", "", "", "", "" };

    public void showPage(int j, bool[] needOk)
    {
        txts = GameData.instance.getOptionNeeds(j);
        if (txts.Length == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        else if(txts.Length == 1 && txts[0].Equals("нч"))
        {
            gameObject.SetActive(false);
            return;
        }
        for (int i = 0; i < text.Length; i++)
        {
            if (i < txts.Length)
            {
                text[i].text = txts[i];
                text[i].gameObject.SetActive(true);
                text[i].color = needOk[i] ? Color.black : Color.red;
            }
            else
            {
                text[i].gameObject.SetActive(false);
                text[i].color = Color.black;
            }
        }
        gameObject.SetActive(true);
    }

    public void closePage()
    {
        gameObject.SetActive(false);
    }


    void Update()
    {
        
    }
}

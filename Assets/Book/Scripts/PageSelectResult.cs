using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageSelectResult : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] text;
    private string[] txts = { "", "", "", "", "", "" };

    public void setPage(int j)
    {
        txts = GameData.instance.getOptionResults(j);
        for (int i = 0; i < text.Length; i++)
        {
            if (i < txts.Length)
            {
                text[i].text = txts[i];
                text[i].gameObject.SetActive(true);
            }
            else
            {
                text[i].gameObject.SetActive(false);
            }
        }
        gameObject.SetActive(true);
    }

    public void closePage()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}

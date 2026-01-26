using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int panelId;//0 故事 1 属性 2 家族 3 角色

    [SerializeField]
    private GameObject panelMessage;
    [SerializeField]
    private GameObject panelFamily;
    [SerializeField]
    private GameObject panelRelo;


    public void turnPanel(int i)
    {
        switch (i)
        {
            case 0:
                panelMessage.SetActive(false);
                panelFamily.SetActive(false);
                panelRelo.SetActive(false);
                break;
            case 1:
                panelMessage.SetActive(true);
                panelFamily.SetActive(false);
                panelRelo.SetActive(false);
                break;
            case 2:
                panelMessage.SetActive(false);
                panelFamily.SetActive(true);
                panelRelo.SetActive(false);
                break;
            case 3:
                panelMessage.SetActive(false);
                panelFamily.SetActive(false);
                panelRelo.SetActive(true);
                break;
            default:
                panelMessage.SetActive(false);
                panelFamily.SetActive(false);
                panelRelo.SetActive(false);
                break;
        }
    }


    void Start()
    {
        turnPanel(0);
    }


    void Update()
    {
        
    }
}

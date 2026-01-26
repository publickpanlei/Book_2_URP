using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaneIRoles : MonoBehaviour
{
    [SerializeField]
    private GameObject pr;
    [SerializeField]
    private Transform root;

    private List<GameObject> prs = new List<GameObject>();


    private void OnEnable()
    {
        for (int i = 0; i < PlayerData.instance.roles.Count; i++)
        {
            GameObject a = Instantiate(pr, root);
            a.GetComponent<PanelRole>().initThis(PlayerData.instance.roles[i].rName, PlayerData.instance.roles[i].job,
                PlayerData.instance.roles[i].born, PlayerData.instance.roles[i].know,
                PlayerData.instance.roles[i].friendly, PlayerData.instance.roles[i].buff);
            prs.Add(a);
        }
    }
    private void OnDisable()
    {
        for (int i = prs.Count - 1; i >= 0; i--) 
        {
            Destroy(prs[i]);
        }
        prs.Clear();
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }
}

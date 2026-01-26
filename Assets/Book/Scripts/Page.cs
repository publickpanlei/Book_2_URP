using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Page : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textTitle;
    [SerializeField]
    private TextMeshProUGUI textTxt;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Image imageRole;
    [SerializeField]
    private TextMeshProUGUI textRole;
    [SerializeField]
    private TextMeshProUGUI textTip;
    [SerializeField]
    private ButtonSelect[] selects;

    /// <summary>
    /// ½²Êö
    /// </summary>
    public void setPage(string title, string txt, string _image)
    {
        textTitle.text = title;
        textTxt.text = txt;
        setImage(_image);
    }
    /// <summary>
    /// ¶Ô»°
    /// </summary>
    public void setPage(string title, string txt, string _image, string role)
    {
        textTitle.text = title;
        textTxt.text = txt;
        setImage(_image);
        textRole.text = role;
        setRoleImage(role);

    }
    /// <summary>
    /// Ñ¡Ôñ
    /// </summary>
    public void setPageSelect()
    {

        for (int i = 0; i < selects.Length; i++)
        {
            if (i < GameData.instance.bookData[PlayerData.instance.storyIndex].choose.Length) 
            {
                selects[i].gameObject.SetActive(true);
                selects[i].setText(GameData.instance.bookData[PlayerData.instance.storyIndex].choose[i].chooseName);
            }
            else
            {
                selects[i].gameObject.SetActive(false);
            }
        }
    }
    public void setPageSelectTip(int i)
    {
        textTip.text = i >= 0 ? GameData.instance.getOptionTip(i) : "";
    }
    /// <summary>
    /// ¿íÍ¼
    /// </summary>
    public void setPageWide(string title, string txt, string _image)
    {
        textTitle.text = title;
        textTxt.text = txt;
        setImage(_image);

    }
    /// <summary>
    /// Õ­Í¼
    /// </summary>
    public void setPageNarrow(string title, string txt, string _image)
    {
        textTitle.text = title;
        textTxt.text = txt;
        setImage(_image);

    }
    private void setImage(string s)
    {
        Sprite sprite = Resources.Load<Sprite>("Images/"+s);
        if (sprite != null)
        {
            image.sprite = sprite;
        }
    }
    private void setRoleImage(string s)
    {
        Sprite sprite = Resources.Load<Sprite>("Roles/" + s);
        if (sprite != null)
        {
            imageRole.sprite = sprite;
        }
    }
    public void lastPage()
    {
        PageHQ.instance.lastPage();

    }
    public void nextPage()
    {
        PageHQ.instance.nextPage();
    }
    void Start()
    {
        
    }

    

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public int selectId;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Button Button;
    private bool[] needOk = { true, true, true, true, true, true };
    private string[] needTxts = { "", "", "", "", "", "" };
    private string[] resultTxts = { "", "", "", "", "", "" };

    public void setText(string _option)
    {
        text.text = _option;
        resetButton();
        needTxts = GameData.instance.getOptionNeeds(selectId);
        resultTxts = GameData.instance.getOptionResults(selectId);
        Button.interactable = ifOpen(_option);
    }
    // 鼠标进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        PageHQ.instance.showPageResultAndNeed(selectId, needOk);
    }

    // 鼠标离开时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        PageHQ.instance.closePageResultAndNeed();
    }
    private void resetButton()
    {
        for (int i = 0; i < needOk.Length; i++)
        {
            needOk[i] = true;
        }
    }
    public bool ifOpen(string _option)
    {
        if (needTxts.Length == 0)
        {
            return true;
        }
        else if (needTxts.Length == 1 && needTxts[0].Equals("无"))
        {
            return true;
        }
        for (int i = 0; i < needTxts.Length; i++)
        {
            needOk[i] = PlayerData.instance.ifOk(needTxts[i]);
        }
        for (int i = 0; i < needOk.Length; i++)
        {
            if (needOk[i] == false)
            {
                return false;
            }
        }
        return true;
    }

    public void OnClick()
    {
        //for (int i = 0; i < resultTxts.Length; i++)
        //{
        //    if (resultTxts[i].Length > 0)
        //    {
        //        PlayerData.instance.setPlayerDate(resultTxts[i]);
        //    }
        //}
        PageHQ.instance.inSelect(selectId);

    }
    void Start()
    {

    }


    void Update()
    {

    }
}

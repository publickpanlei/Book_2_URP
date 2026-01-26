using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PageHQ : MonoBehaviour
{
    public static PageHQ instance;
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

    [SerializeField]
    private Page[] pages;//左侧的: 0讲述 1对话 //右侧的: 2选择 //顶层的: 3横图 4窄图
    [SerializeField]
    private Image image;//右图
    [SerializeField]
    private TextMeshProUGUI textChapter;
    [SerializeField]
    private TextMeshProUGUI textYear;
    [SerializeField]
    private PageSelectResult pageSelectResult;
    [SerializeField]
    private PageSelectNeed pageSelectNeed;
    [SerializeField]
    private Button buttonLast;
    [SerializeField]
    private Button buttonLastWide;
    [SerializeField]
    private Button buttonNext;

    private int type;

    private void turnPage()
    {
        type = getPageType();
        switch (type)
        {
            case 0:
                pages[0].gameObject.SetActive(true);
                pages[1].gameObject.SetActive(false);
                if (PlayerData.instance.inSelect == 2)
                {
                    pages[0].setPage(GameData.instance.bookData[PlayerData.instance.storyIndex].storyName,
                        GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].text,
                        GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].image);
                }
                else
                {
                    pages[0].setPage(GameData.instance.bookData[PlayerData.instance.storyIndex].storyName,
                        GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].text,
                        GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].image);
                }
                image.gameObject.SetActive(true);
                pages[2].gameObject.SetActive(false);
                pages[3].gameObject.SetActive(false);
                pages[4].gameObject.SetActive(false);
                break;
            case 1:
                pages[0].gameObject.SetActive(false);
                pages[1].gameObject.SetActive(true);
                if (PlayerData.instance.inSelect == 2)
                {
                    pages[1].setPage(GameData.instance.bookData[PlayerData.instance.storyIndex].storyName,
                       GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].text,
                       GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].image,
                       GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].role);
                }
                else
                {
                    pages[1].setPage(GameData.instance.bookData[PlayerData.instance.storyIndex].storyName,
                        GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].text,
                        GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].image,
                        GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].role);
                }
                image.gameObject.SetActive(true);
                pages[2].gameObject.SetActive(false);
                pages[3].gameObject.SetActive(false);
                pages[4].gameObject.SetActive(false);
                break;
            case 2:
                image.gameObject.SetActive(false);
                pages[2].gameObject.SetActive(true);
                pages[2].setPageSelect();
                pages[3].gameObject.SetActive(false);
                pages[4].gameObject.SetActive(false);
                break;
            case 3:
                pages[3].gameObject.SetActive(true);
                pages[4].gameObject.SetActive(false);
                pages[3].setPageWide(GameData.instance.bookData[PlayerData.instance.storyIndex].storyName,
                    GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].text,
                    GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].image);
                break;
            case 4:
                pages[3].gameObject.SetActive(false);
                pages[4].gameObject.SetActive(true);
                pages[4].setPageNarrow(GameData.instance.bookData[PlayerData.instance.storyIndex].storyName,
                    GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].text,
                    GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].image);
                break;
        }
    }

    public void nextStory()
    {
        if (PlayerData.instance.storyIndex < GameData.instance.bookData.Count - 1)
        {
            PlayerData.instance.storyIndex++;
            PlayerData.instance.pageIndex = 0;
            PlayerData.instance.inSelect = 0;
            PlayerData.instance.selectIndex = 0;
            PlayerData.instance.selectPageIndex = 0;
            buttonLast.interactable = false;
            buttonLastWide.interactable = false;
            buttonNext.interactable = true;
            turnPage();
        }
        else
        {
            Debug.Log("全书结束");
        }
    }
    public void lastPage()
    {
        if (PlayerData.instance.inSelect <= 1)
        {
            if (PlayerData.instance.pageIndex > 0)
            {
                PlayerData.instance.pageIndex--;
                PlayerData.instance.inSelect = 0;
                if (PlayerData.instance.pageIndex == 0)
                {
                    buttonLast.interactable = false;
                    buttonLastWide.interactable = false;
                }
                buttonNext.interactable = true;
                turnPage();
            }
            else
            {
                Debug.Log("已经是第一页");
            }
        }
        else if (PlayerData.instance.inSelect == 2)
        {
            if (PlayerData.instance.selectPageIndex > 0)
            {
                PlayerData.instance.selectPageIndex--;
                if (PlayerData.instance.selectPageIndex == 0)
                {
                    buttonLast.interactable = false;
                }
                buttonNext.interactable = true;
                turnPage();
            }
            else
            {
                Debug.Log("已经是第一页");
            }
        }
    }
    public void nextPage()
    {
        if (PlayerData.instance.inSelect <= 1)
        {
            if (PlayerData.instance.pageIndex < GameData.instance.bookData[PlayerData.instance.storyIndex].page.Length - 1)
            {
                PlayerData.instance.pageIndex++;
                buttonLast.interactable = true;
                buttonLastWide.interactable = true;
                turnPage();
            }
            else if (PlayerData.instance.pageIndex == GameData.instance.bookData[PlayerData.instance.storyIndex].page.Length - 1)
            {
                if (GameData.instance.bookData[PlayerData.instance.storyIndex].choose.Length > 0 && type <= 2) 
                {
                    PlayerData.instance.inSelect = 1;//进入选择页面
                    buttonNext.interactable = false;
                    turnPage();
                }
                else
                {
                    Debug.Log("故事结束");
                    updateResult();
                    nextStory();
                }
            }
        }
        else if (PlayerData.instance.inSelect == 2)
        {
            if (PlayerData.instance.selectPageIndex < GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page.Length - 1)
            {
                PlayerData.instance.selectPageIndex++;
                buttonLast.interactable = true;
                turnPage();
            }
            else if (PlayerData.instance.selectPageIndex == GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page.Length - 1)
            {
                Debug.Log("故事结束");
                updateResult();
                nextStory();
            }
        }
    }
    public void updateResult()
    {
        if (GameData.instance.bookData[PlayerData.instance.storyIndex].choose.Length == 0)
        {
            return;
        }
        for (int i = 0; i < GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].result.Length; i++) 
        {
            string a = GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].result[i];

            // 检查是否包含+或-
            if (!a.Contains("+") && !a.Contains("-"))
            {
                Debug.LogWarning($"指令 '{a}' 不包含 '+' 或 '-' 操作符");
                continue;
            }

            char operatorChar = a.Contains("+") ? '+' : '-';
            int operatorIndex = a.IndexOf(operatorChar);

            if (operatorIndex <= 0 || operatorIndex >= a.Length - 1)
            {
                Debug.LogWarning($"指令 '{a}' 格式错误");
                continue;
            }

            string leftPart = a.Substring(0, operatorIndex).Trim();
            string rightPart = a.Substring(operatorIndex + 1).Trim();
            bool isAddition = operatorChar == '+';

            // 1. 检查是否包含"标签"
            if (a.Contains("标签"))
            {
                // 格式: "角色名+标签+新标签" 或 "角色名-标签-要移除的标签"
                string[] labelParts = a.Split(new char[] { '+', '-' }, 3);
                if (labelParts.Length < 3)
                {
                    Debug.LogWarning($"标签指令格式错误: {a}");
                    continue;
                }

                string roleName = labelParts[0].Trim();
                string labelName = labelParts[2].Trim();

                var targetRole = PlayerData.instance.roles.FirstOrDefault(r => a.Contains(r.rName));
                if (targetRole != null)
                {
                    if (isAddition)
                    {
                        targetRole.AddBuff(labelName);
                        Debug.Log($"角色 '{roleName}' 添加标签: {labelName}");
                    }
                    else
                    {
                        targetRole.RemoveBuff(labelName);
                        Debug.Log($"角色 '{roleName}' 移除标签: {labelName}");
                    }
                }
                else
                {
                    Debug.LogWarning($"未找到角色: {roleName}");
                }
                continue;
            }

            // 2. 检查是否包含某个角色的名字
            var matchedRole = PlayerData.instance.roles.FirstOrDefault(r => a.Contains(r.rName));
            if (matchedRole != null)
            {
                // 检查是否包含"认识"或"友善"
                if (a.Contains("认识"))
                {
                    if (int.TryParse(rightPart, out int value))
                    {
                        matchedRole.know += isAddition ? value : -value;
                        Debug.Log($"角色 '{matchedRole.rName}' 认识 {(isAddition ? "增加" : "减少")} {value}，当前: {matchedRole.know}");
                    }
                    else
                    {
                        Debug.LogWarning($"无法解析数值: {rightPart}");
                    }
                    continue;
                }

                if (a.Contains("友善"))
                {
                    if (int.TryParse(rightPart, out int value))
                    {
                        matchedRole.friendly += isAddition ? value : -value;
                        Debug.Log($"角色 '{matchedRole.rName}' 友善 {(isAddition ? "增加" : "减少")} {value}，当前: {matchedRole.friendly}");
                    }
                    else
                    {
                        Debug.LogWarning($"无法解析数值: {rightPart}");
                    }
                    continue;
                }
            }

            // 3. 检查attributeOne中的属性
            var matchedAttributeOne = PlayerData.instance.attributeOne.FirstOrDefault(attr => a.Contains(attr.aName));
            if (matchedAttributeOne != null)
            {
                if (int.TryParse(rightPart, out int value))
                {
                    int newValue = matchedAttributeOne.value + (isAddition ? value : -value);
                    // 限制在min和max范围内
                    newValue = Mathf.Clamp(newValue, matchedAttributeOne.min, matchedAttributeOne.max);
                    matchedAttributeOne.value = newValue;
                    Debug.Log($"属性 '{matchedAttributeOne.aName}' {(isAddition ? "增加" : "减少")} {value}，当前: {matchedAttributeOne.value}");
                }
                else
                {
                    Debug.LogWarning($"无法解析数值: {rightPart}");
                }
                continue;
            }

            // 4. 检查attributeFamily中的属性
            var matchedAttributeFamily = PlayerData.instance.attributeFamily.FirstOrDefault(attr => a.Contains(attr.aName));
            if (matchedAttributeFamily != null)
            {
                if (int.TryParse(rightPart, out int value))
                {
                    int newValue = matchedAttributeFamily.value + (isAddition ? value : -value);
                    // 限制在min和max范围内
                    newValue = Mathf.Clamp(newValue, matchedAttributeFamily.min, matchedAttributeFamily.max);
                    matchedAttributeFamily.value = newValue;
                    Debug.Log($"属性 '{matchedAttributeFamily.aName}' {(isAddition ? "增加" : "减少")} {value}，当前: {matchedAttributeFamily.value}");
                }
                else
                {
                    Debug.LogWarning($"无法解析数值: {rightPart}");
                }
                continue;
            }

            // 5. 如果没有匹配到任何内容
            Debug.LogWarning($"未找到匹配的对象执行指令: {a}");
        }
    }
    public void inSelect(int i)
    {
        PlayerData.instance.selectIndex = i;
        closePageResultAndNeed();
        PlayerData.instance.inSelect = 2;
        buttonLast.interactable = false;
        buttonNext.interactable = true;
        PlayerData.instance.selectPageIndex = -1;
       nextPage();
    }
    /// <summary>
    /// 获知page类型
    /// </summary>
    private int getPageType()
    {
        int i = -1;
        if (PlayerData.instance.inSelect == 0)
        {
            if (GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].role.Equals("无") ||
                GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].role.Length == 0)
            {
                i = 0;
            }
            else if (GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].role.Equals("背景叙事"))
            {
                i = 3;
            }
            else if (GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].role.Equals("提示"))
            {
                i = 4;
            }
            else if (GameData.instance.bookData[PlayerData.instance.storyIndex].page[PlayerData.instance.pageIndex].role.Length > 0)
            {
                i = 1;
            }
        }
        else if (PlayerData.instance.inSelect == 1)
        {
            i = 2;
        }
        else if (PlayerData.instance.inSelect == 2)
        {
            if (GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].role.Equals("无") ||
                GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].role.Length == 0)
            {
                i = 0;
            }
            else if (GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].role.Equals("背景叙事"))
            {
                i = 3;
            }
            else if (GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].role.Equals("提示"))
            {
                i = 4;
            }
            else if (GameData.instance.bookData[PlayerData.instance.storyIndex].choose[PlayerData.instance.selectIndex].page[PlayerData.instance.selectPageIndex].role.Length > 0)
            {
                i = 1;
            }
        }
        return i;
    }
    public void showPageResultAndNeed(int i, bool[] needOk)
    {
        pageSelectResult.setPage(i);
        pageSelectNeed.showPage(i, needOk);
        pages[2].setPageSelectTip(i);

    }
    public void closePageResultAndNeed()
    {
        pageSelectResult.closePage();
        pageSelectNeed.closePage();
        pages[2].setPageSelectTip(-1);
    }
    void Start()
    {
        closePageResultAndNeed();
    }


    void Update()
    {
        
    }
}

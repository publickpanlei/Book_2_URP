using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LitJson;
using System.IO;
using System;



public class Attribute
{
    public string aName;
    public int value;
    public int min;
    public int max;
    public Attribute(string a, int v, int _min, int _max)
    {
        aName = a; value = v; min = _min; max = _max;
    }
}
public class Role
{
    public string rName;
    public string job;
    public int born;
    public int know;
    public int friendly;
    public List<string> buff;
    public Role(string r, string j, int b, int k, int f, string[] _buff)
    {
        rName = r; job = j; born = b; know = k; friendly = f; buff = _buff.ToList();
    }
    public void AddBuff(string newBuff)
    {
        buff.Add(newBuff);
    }
    public void RemoveBuff(string buffToRemove)
    {
        buff.Remove(buffToRemove);
    }
}
public class GameData : MonoBehaviour
{
    public static GameData instance;
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
    //**************属性数据**************
    #region
    public List<Attribute> attributeOne = new List<Attribute>();
    public List<Attribute> attributeFamily = new List<Attribute>();
    private JsonData attributeJsonDataOne;
    private JsonData attributeJsonDataFamily;

    public void initAttributesData()
    {
        attributeJsonDataOne = JsonHQ.instance.GetAttributeData()["个人"];
        attributeJsonDataFamily = JsonHQ.instance.GetAttributeData()["家族"];
        Debug.Log("个人属性数量: " + attributeJsonDataOne.Count + " 家庭属性数量: " + attributeJsonDataFamily.Count);
        // 清空现有数据
        attributeOne.Clear();
        if (attributeJsonDataOne != null && attributeJsonDataOne.IsArray)
        {
            for (int i = 0; i < attributeJsonDataOne.Count; i++)
            {
                JsonData oneJson = attributeJsonDataOne[i];
                try
                {
                    string aName = oneJson["属性名"].ToString();
                    int value = int.Parse(oneJson["初始值"].ToString());
                    int min = int.Parse(oneJson["下限"].ToString());
                    int max = int.Parse(oneJson["上限"].ToString());
                    Attribute data = new Attribute(aName, value, min, max);
                    attributeOne.Add(data);
                }
                 catch (System.Exception e)
                {
                    Debug.LogError($"解析第{i}个个人属性数据时出错: {e.Message}");
                }
            }
        }
        attributeFamily.Clear();
        if (attributeJsonDataFamily != null && attributeJsonDataFamily.IsArray)
        {
            for (int i = 0; i < attributeJsonDataFamily.Count; i++)
            {
                JsonData oneJson = attributeJsonDataFamily[i];
                try
                {
                    string aName = oneJson["属性名"].ToString();
                    int value = int.Parse(oneJson["初始值"].ToString());
                    int min = int.Parse(oneJson["下限"].ToString());
                    int max = int.Parse(oneJson["上限"].ToString());
                    Attribute data = new Attribute(aName, value, min, max);
                    attributeFamily.Add(data);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"解析第{i}个家庭属性数据时出错: {e.Message}");
                }
            }
        }
        Debug.Log("属性数据初始化完成，共加载 " + attributeOne.Count + " 个个人属性和 " + attributeFamily.Count + " 个家族属性");
    }
    #endregion
    //**************角色数据**************
    #region
    public List<Role> roles = new List<Role>();
    private JsonData roleJsonData;
    public void initRoleData()
    {
        roleJsonData = JsonHQ.instance.GetAttributeData()["角色"];
        Debug.Log("角色数量: " + roleJsonData.Count);
        // 清空现有数据
        roles.Clear();
        if (roleJsonData != null && roleJsonData.IsArray)
        {
            for (int i = 0; i < roleJsonData.Count; i++)
            {
                JsonData roleJson = roleJsonData[i];
                try
                {
                    string rName = roleJson["人名"].ToString();
                    string job = roleJson["身份"].ToString();
                    int born = int.Parse(roleJson["出生"].ToString());
                    int know = int.Parse(roleJson["认识"].ToString());
                    int friendly = int.Parse(roleJson["友善"].ToString());
                    // 解析标签数组
                    string[] buff = new string[roleJson["关系标签"].Count];
                    for (int j = 0; j < roleJson["关系标签"].Count; j++)
                    {
                        buff[j] = roleJson["关系标签"][j].ToString();
                    }
                    Role data = new Role(rName, job, born, know, friendly, buff);
                    roles.Add(data);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"解析第{i}个角色数据时出错: {e.Message}");
                }
            }
        }
        Debug.Log("角色数据初始化完成，共加载 " + roles.Count + " 个角色");
    }
    #endregion
    //**************书页数据**************
    #region
    public struct Page
    {
        public string role;
        public string image;
        public string text;
    }
    public struct Choose
    {
        public string chooseName;
        public string text;
        public string[] premise;
        public string[] result;
        public Page[] page;
    }
    public class BookData
    {
        public string storyName;
        public string note;
        public string[] premise;
        public Page[] page;
        public Choose[] choose;
        public BookData(string p, string n, string[] a)
        {
            storyName = p; note = n; premise = a;
        }
    }
    public List<BookData> bookData = new List<BookData>();
    private JsonData bookJsonData;
    public void initBookData()
    {
      
        bookJsonData = JsonHQ.instance.GetBookData()["事件"];
        Debug.Log("书页数量: " + bookJsonData.Count);

        // 清空现有数据
        bookData.Clear();

        // 遍历JSON数据并转换为BookData对象
        if (bookJsonData != null && bookJsonData.IsArray)
        {
            for (int i = 0; i < bookJsonData.Count; i++)
            {
                JsonData bookJson = bookJsonData[i];

                try
                {
                    // 解析基本字段
                    string pageName = bookJson["事件名"].ToString();
                    string note = bookJson["备注"].ToString();

                    // 解析前提条件数组
                    string[] premise = new string[bookJson["前提"].Count];
                    for (int j = 0; j < bookJson["前提"].Count; j++)
                    {
                        premise[j] = bookJson["前提"][j].ToString();
                    }

                    // 创建BookData对象
                    BookData data = new BookData(pageName, note, premise);

                    // 解析叙事部分
                    if (bookJson["叙事"].IsArray)
                    {
                        List<Page> pageList = new List<Page>();
                        for (int j = 0; j < bookJson["叙事"].Count; j++)
                        {
                            JsonData pageJson = bookJson["叙事"][j];
                            Page narrate = new Page();

                            narrate.role = pageJson["角色"].ToString();
                            narrate.image = pageJson["图片"].ToString();

                            // 处理叙述/文本字段的不一致性
                            if (pageJson["叙述"] != null)
                            {
                                narrate.text = pageJson["叙述"].ToString();
                            }
                            else if (pageJson["文本"] != null)
                            {
                                narrate.text = pageJson["文本"].ToString();
                            }
                            else
                            {
                                narrate.text = "";
                            }

                            pageList.Add(narrate);
                        }
                        data.page = pageList.ToArray();
                    }

                    // 解析选择部分
                    if (bookJson["选择"].IsArray)
                    {
                        List<Choose> chooseList = new List<Choose>();
                        for (int j = 0; j < bookJson["选择"].Count; j++)
                        {
                            JsonData chooseJson = bookJson["选择"][j];
                            Choose choose = new Choose();

                            choose.chooseName = chooseJson["选项名"].ToString();
                            choose.text = chooseJson["选项叙述"].ToString();

                            // 解析条件数组
                            List<string> choosePremise = new List<string>();
                            for (int k = 0; k < chooseJson["条件"].Count; k++)
                            {
                                choosePremise.Add(chooseJson["条件"][k].ToString());
                            }
                            choose.premise = choosePremise.ToArray();

                            // 解析结果数组
                            List<string> results = new List<string>();
                            for (int k = 0; k < chooseJson["结果"].Count; k++)
                            {
                                results.Add(chooseJson["结果"][k].ToString());
                            }
                            choose.result = results.ToArray();

                            // 解析选择内的叙事
                            if (chooseJson["叙事"].IsArray)
                            {
                                List<Page> chooseNarrateList = new List<Page>();
                                for (int k = 0; k < chooseJson["叙事"].Count; k++)
                                {
                                    JsonData chooseNarrateJson = chooseJson["叙事"][k];
                                    Page chooseNarrate = new Page();

                                    chooseNarrate.role = chooseNarrateJson["角色"].ToString();
                                    chooseNarrate.image = chooseNarrateJson["图片"].ToString();

                                    // 处理叙述/文本字段的不一致性
                                    if (chooseNarrateJson["叙述"] != null)
                                    {
                                        chooseNarrate.text = chooseNarrateJson["叙述"].ToString();
                                    }
                                    else if (chooseNarrateJson["文本"] != null)
                                    {
                                        chooseNarrate.text = chooseNarrateJson["文本"].ToString();
                                    }
                                    else
                                    {
                                        chooseNarrate.text = "";
                                    }

                                    chooseNarrateList.Add(chooseNarrate);
                                }
                                choose.page = chooseNarrateList.ToArray();
                            }
                            else
                            {
                                choose.page = new Page[0];
                            }

                            chooseList.Add(choose);
                        }
                        data.choose = chooseList.ToArray();
                    }
                    else
                    {
                        data.choose = new Choose[0];
                    }

                    bookData.Add(data);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"解析第{i}个书页数据时出错: {e.Message}");
                }
            }
        }

        Debug.Log("书页数据初始化完成，共加载 " + bookData.Count + " 个书页");
    }
    public string getOptionTip(int i)
    {
        return bookData[PlayerData.instance.storyIndex].choose[i].text;
    }
    public string[] getOptionNeeds(int i)
    {
        return bookData[PlayerData.instance.storyIndex].choose[i].premise;
    }
    public string[] getOptionResults(int i)
    {
        return bookData[PlayerData.instance.storyIndex].choose[i].result;
    }
    #endregion




    //**************常数系数************** 
    //public int[] npcDataX = { 1, 1, 10, 10 };//速度 攻击 血量 内力 系数
    //public int levUpExpA = 100;//升级所需经验基数
    //public int levUpExpX = 100;//升级所需经验系数
    //public int npcSpitExpA = 20;//打败npc获得经验基数
    //public int npcSpitExpX = 20;//打败npc获得经验系数

    //**********************************************************


    void Start()
    {
        
    }


    void Update()
    {

    }
}

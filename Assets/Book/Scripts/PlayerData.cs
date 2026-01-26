using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using LitJson;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            savePath = Path.Combine(Application.persistentDataPath, "save_" + saveId + ".json");
            Debug.Log("存档位置 " + savePath);
            if (!File.Exists(savePath))
            {
                // 创建一个空的 JSON 文件
                File.WriteAllText(savePath, "{}"); // 写入一个空对象作为初始内容
                Debug.Log("存档不存在，创建一个。");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    internal object getPlayerDates(int i)
    {
        throw new NotImplementedException();
    }



    //语言 0 简中 1 英语
    public int languageId = 0;

    private int saveId = 0;

    string savePath;//= Path.Combine(Application.persistentDataPath, "save_"+ saveId + ".json");

    private JsonData jsonObject;

    public List<Attribute> attributeOne = new List<Attribute>();
    public List<Attribute> attributeFamily = new List<Attribute>();
    public List<Role> roles = new List<Role>();

    public int storyIndex = -1;//故事序号
    public int pageIndex;//故事中的叙事页数
    public int inSelect;//选择状态 0 未选 1正在选 2选完
    public int selectIndex;//选择的选项序号
    public int selectPageIndex = -1;//选项中的叙事页数
    public void newData()
    {
        Debug.Log("开启新游戏");
        attributeOne = GameData.instance.attributeOne;
        attributeFamily = GameData.instance.attributeFamily;
        roles = GameData.instance.roles;
        PageHQ.instance.nextStory();



    }
    public void saveData()
    {
        jsonObject = new JsonData();
        jsonObject["version"] = Application.version;




        //保存
        string jsonString = jsonObject.ToJson();
        File.WriteAllText(savePath, jsonString);
        Debug.Log("Save成功");
    }

    public bool ifOk(string v)
    {
        char[] operators = { '>', '<', '=' };
        int operatorIndex = v.IndexOfAny(operators);

        if (operatorIndex < 0) return false;

        string a = v.Substring(0, operatorIndex);
        char op = v[operatorIndex];
        int.TryParse(v.Substring(operatorIndex + 1), out int i);

        // 合并两个列表进行查找
        var allAttributes = attributeOne.Concat(attributeFamily);
        var attribute = allAttributes.FirstOrDefault(attr => attr.aName == a);

        if (attribute == null) return false;

        return op switch
        {
            '>' => attribute.value > i,
            '<' => attribute.value < i,
            '=' => attribute.value == i,
            _ => false
        };
    }

    public void loadData()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string jsonString = File.ReadAllText(savePath);
                Debug.Log("加载存档开始");
                JsonData jsonObject = JsonMapper.ToObject(jsonString);
                JsonData jsonDataVersion = jsonObject["version"];
                Debug.Log("存档版本 " + jsonDataVersion.ToString());




                Debug.Log("加载存档完毕");
            }
            catch (Exception e)
            {
                Debug.LogError("读取失败: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("存档文件不存在！");
        }
    }

    internal void setPlayerDate(string v)
    {
        throw new NotImplementedException();
    }

    void Start()
    {

    }

   

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    Debug.Log("playerDates " + roles[0].rName + " " + roles[0].job + " " + roles[0].buff[0]);
        //}
    }
    //***********成就***********
    private bool ach = true;

    public void setAch(int i)
    {
        if (ach)
        {
            Debug.Log("ach " + i);
            //   SteamManager.s_instance.setAch(i);
        }
    }
}

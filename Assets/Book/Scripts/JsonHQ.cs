using UnityEngine;
using LitJson;
using System.IO;

public class JsonHQ : MonoBehaviour
{
    public static JsonHQ instance;

    // 存储加载的JSON数据
    private JsonData attributesData;
    private JsonData bookData;

    private void Awake()
    {
        // 单例模式
        if (instance == null)
        {
            instance = this;
        //    DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

       
    }
    private void Start()
    {
        // 加载JSON文件
        LoadJsonFiles();
    }

    private void LoadJsonFiles()
    {
        // 加载属性文件
        TextAsset attributesFile = Resources.Load<TextAsset>("属性");
        if (attributesFile != null)
        {
            attributesData = JsonMapper.ToObject(attributesFile.text);
            Debug.Log("属性JSON加载成功");
        }
        else
        {
            Debug.LogError("属性文件加载失败，请确保文件位于Resources文件夹下");
        }
        GameData.instance.initAttributesData();
        GameData.instance.initRoleData();
        // 加载事件文件
        TextAsset bookFile = Resources.Load<TextAsset>("书页");
        if (bookFile != null)
        {
            bookData = JsonMapper.ToObject(bookFile.text);
            Debug.Log("书页JSON加载成功");
        }
        else
        {
            Debug.LogError("事件文件加载失败，请确保文件位于Resources文件夹下");
        }
        GameData.instance.initBookData();
    }

    // 提供外部访问接口
    public JsonData GetAttributeData()
    {
        return attributesData;
    }

    public JsonData GetBookData()
    {
        return bookData;
    }
}
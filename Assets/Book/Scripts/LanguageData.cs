using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageData : MonoBehaviour
{
    public static LanguageData instance;
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
    //*************分支选择文本*************
    public class LanguageSentence
    {
        //0 英文 1 中文
        public string[] sentence = new string[2] { "", "" };
        public string getSentence(int i)
        {
            return sentence[i];
        }
        public void setSentence(int i,string s)
        {
            sentence[i] = s;
        }
    }
    private LanguageSentence[] gameText;
    public string getGameText(int i)
    {
        if (i == -1)
            return "";
        return gameText[i].getSentence(PlayerData.instance.languageId);
    }
    public void initGameText(int langNum, int textNum)
    {
        gameText = new LanguageSentence[textNum];
        for (int i = 0; i < gameText.Length; i++)
        {
            gameText[i] = new LanguageSentence();
            gameText[i].sentence = new string[langNum];
            for (int j = 0; j < langNum; j++)
            {
                gameText[i].sentence[j] = "";
            }
        }
    }
    public void loadGameText(int i, int languageId, string s)
    {
        gameText[i].setSentence(languageId, s);
    }
    //**************************************


    //*************底端字幕文本*************
    public class VideoLanguageSentence
    {
        //0 英文 1 中文
        public string[] sentence = new string[2] { "", "" };
        public int videoId;
        public float videoTime;
        public float videoTimeOver;
        public int index = -1;
        public string getSentence(int i)
        {
            return sentence[i];
        }
        public void setSentence(int i, string s)
        {
            sentence[i] = s;
        }
        public float getVideoTime()
        {
            return videoTime;
        }
        public float getVideoTimeOver()
        {
            return videoTimeOver;
        }
        public void setVideo(int i, float f, float m)
        {
            videoId = i;
            videoTime = f;
            videoTimeOver = m;
        }
    }
    public class VideoSentenceGroup
    {
        public int voideId;
        public int longNum;
        public List<VideoLanguageSentence> videoText = new List<VideoLanguageSentence>();
        public void setVideoGroup(int i, int j)
        {
            voideId = i;
            longNum = j;
        }
        public void addSentence(int i, int j, string s, int vi, float vt, float vto)
        {
            foreach (VideoLanguageSentence b in videoText)
            {
                if (b.index == i)
                {
                    b.setSentence(j, s);
                    return;
                }
            }
            VideoLanguageSentence a = new VideoLanguageSentence();
            a.index = i;
            a.setSentence(j, s);
            a.setVideo(vi, vt, vto);
            videoText.Add(a);
        }
    }
    private VideoSentenceGroup[] videoGroups;
    public void initVideoText(int langNum, int videoGroupNum)
    {
        videoGroups = new VideoSentenceGroup[videoGroupNum];
    //    Debug.Log("000  videoGroups.Length " + videoGroups.Length);
        for (int i = 0; i < videoGroups.Length; i++)
        {
            videoGroups[i] = new VideoSentenceGroup();
            videoGroups[i].setVideoGroup(i, langNum);
        }
    }
    public void loadVideoText(int i, int languageId, string s, int vi, float vt, float vto)
    {
    //    Debug.Log("111  loadVideoText " + languageId+" "+s+" "+vi+" "+vt+" ");
        if (vi >= 0 && vt >= 0)
        {
            videoGroups[vi].addSentence(i, languageId, s, vi, vt, vto);
        }
    }
    public VideoSentenceGroup getVSG(int i)
    {
        return videoGroups[i];
    }

    //**************************************
}

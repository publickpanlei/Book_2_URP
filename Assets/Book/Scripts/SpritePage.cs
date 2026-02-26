using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpritePage : MonoBehaviour
{

    public CanvasGroup[] canvasGroups;
    public GameObject spPage;
    public Animator spPageAnimator;

    // 可调整的时间参数，方便在Inspector中调试
    private float fadeOutDuration = 1f; // 当前页面淡出时间
    private float pageTurnDuration = 2.1f; // 翻页动画的持续时间（应与Animator中动画长度匹配）
    private float fadeInDuration = 1f; // 新页面淡入时间

    void Start()
    {
    }
    public void TurnPage()
    {
        ////1,先让canvasGroups的透明度降低为0，用时1秒。
        //for (int i = 0; i < canvasGroups.Length; i++)
        //{
        //    canvasGroups[i].alpha = 0;
        //}
        ////2，执行数据函数，同时启动翻页动画
        //turnData();
        //spPage.SetActive(true);
        //spPageAnimator.SetTrigger("Turn");
        ////3，翻页动画持续时间为2秒，结束之后，再次隐藏spPage，并让canvasGroups的透明度恢复为1，用时1秒。
        //spPage.SetActive(false);
        //for (int i = 0; i < canvasGroups.Length; i++)
        //{
        //    canvasGroups[i].alpha = 1;
        //}
        // 使用DOTween序列来精确控制步骤顺序和时间
        Sequence turnSequence = DOTween.Sequence();

        // 步骤1: 当前页面（CanvasGroups）淡出
        foreach (CanvasGroup cg in canvasGroups)
        {
            turnSequence.Join(cg.DOFade(0, fadeOutDuration)); // Join表示与序列中当前步骤并行执行
        }

        // 步骤2: 在当前页面淡出后，执行数据切换并准备播放翻页动画
        turnSequence.AppendCallback(() =>
        {
            turnData(); // 调用数据切换
            spPage.SetActive(true); // 激活翻页动画对象
            spPageAnimator.SetTrigger("Turn"); // 触发翻页动画
        });

        // 步骤3: 等待翻页动画播放的持续时间
        turnSequence.AppendInterval(pageTurnDuration);

        // 步骤4: 翻页动画结束后，隐藏动画对象，并让新页面（CanvasGroups）淡入
        turnSequence.AppendCallback(() =>
        {
            spPage.SetActive(false);
        });
        foreach (CanvasGroup cg in canvasGroups)
        {
            turnSequence.Join(cg.DOFade(1, fadeInDuration));
        }

        // 可选的：序列完成后执行其他操作
        turnSequence.OnComplete(()=>{
            PageHQ.instance.openButtonNext();
            Debug.Log("翻页完成");
        });

    }
    public void turnData()
    {
        //数据函数
        PageHQ.instance.nextStoryData();
    }
    void Update()
    {
        
    }
}

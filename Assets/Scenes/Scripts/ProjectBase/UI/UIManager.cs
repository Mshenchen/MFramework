using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI层级枚举
/// </summary>
public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System,
}
/// <summary>
/// UI管理器
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;
    public RectTransform canvas;
    public UIManager()
    {
        //创建Canvas 让其过场景的时候 不被移除
        GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);
        //找到各层
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");
        
        //创建EventSystem 让其过场景的时候  不被移除
        obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }
    /// <summary>
    /// 显示面板
    /// </summary>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪一层</param>
    /// <param name="callBack">当面板预设体创建成功后 你要做的事</param>
    /// <typeparam name="T">面板脚本类型</typeparam>
    public void ShowPanel<T>(string panelName,E_UI_Layer layer = E_UI_Layer.Mid,UnityAction<T> callBack = null) where T : BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            if (callBack != null)
                callBack(panelDic[panelName] as T);
            return;
        }
        ResMgr.GetInstance().LoadAsync<GameObject>("UI/"+panelName, (obj) =>
        {
            Transform father = bot;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
            }
            //设置父对象  设置相对位置和大小
            obj.transform.SetParent(father);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.zero;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;
            //得到预制体身上的面板脚本
            T panel = obj.GetComponent<T>();
            
            if (callBack != null)
                callBack(panel);
            panel.ShowMe();
            //将面板存起来
            panelDic.Add(panelName,panel);
        });    
    }

    public void HidePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HideMe();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }
    /// <summary>
    /// 得到已经显示的面板
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetPanel<T>(string name) where T : BasePanel
    {
        if (panelDic.ContainsKey(name))
            return panelDic[name] as T;
        return null;
    }
}

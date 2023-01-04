using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 事件中心  单例模式对象
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();
    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">处理事件的委托函数</param>
    public void AddEventListener(string name, UnityAction<object> action)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name] += action;
        }
        else
        {
            eventDic.Add(name,action);
        }
    }

    public void RemoveEventListener(string name, UnityAction<object> action)
    {
        if (eventDic.ContainsKey(name))
            eventDic[name] -= action;
    }
    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">触发事件的名字</param>
    public void EventTrigger(string name,object info)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name].Invoke(info);
        }
    }
    /// <summary>
    /// 清空事件中心
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}

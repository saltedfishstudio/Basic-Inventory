using System;
using System.Collections.Generic;

public class EventDispatcher
{
	static Dictionary<string, List<EventBase>> actionList = new Dictionary<string, List<EventBase>>();

	public static void Reset()
	{
		actionList = new Dictionary<string, List<EventBase>>();
	}

	#region Args0

	public static void Register(string id, Action action)
	{
		if (!actionList.TryGetValue(id, out var actions))
		{
			actions = new List<EventBase>();
		}
		
		actions.Add(new Event(action));
		actionList[id] = actions;
	}

	public static void Unregister(string id, Action action)
	{
		if (!actionList.TryGetValue(id, out var actions))
		{
			return;
		}

		for (int i = 0; i < actions.Count; i++)
		{
			EventBase eventBase = actions[i];
			if ((eventBase as Event).IsIdentical(action))
			{
				actions.RemoveAt(i);
				break;
			}
		}
		
		actionList[id] = actions;
	}

	public static void Execute(string id)
	{
		if (!actionList.TryGetValue(id, out var actions))
		{
			return;
		}

		foreach (var action in actions)
		{
			(action as Event).Invoke();
		}
	}
	
	#endregion

	#region Args1

	public static void Register<T1>(string id, Action<T1> action)
	{
		if (!actionList.TryGetValue(id, out var actions))
		{
			actions = new List<EventBase>();
		}
		
		actions.Add(new Event<T1>(action));
		actionList[id] = actions;
	}

	public static void Unregister<T1>(string id, Action<T1> action)
	{
		if (!actionList.TryGetValue(id, out var actions))
		{
			return;
		}

		for (int i = 0; i < actions.Count; i++)
		{
			EventBase eventBase = actions[i];
			if ((eventBase as Event<T1>).IsIdentical(action))
			{
				actions.RemoveAt(i);
				break;
			}
		}
		
		actionList[id] = actions;
	}

	public static void Execute<T1>(string id, T1 arg)
	{
		if (!actionList.TryGetValue(id, out var actions))
		{
			return;
		}

		foreach (var action in actions)
		{
			(action as Event<T1>).Invoke(arg);
		}
	}
	
	#endregion

}
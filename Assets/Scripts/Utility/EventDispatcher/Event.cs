using System;

public class Event : EventBase
{
	event Action action;

	public Event(Action action)
	{
		this.action = action;
	}

	public bool IsIdentical(Action action)
	{
		return this.action == action;
	}

	public void Invoke()
	{
		action();
	}
}

public class Event<T1> : EventBase
{
	event Action<T1> action;
	
	public Event(Action<T1> action)
	{
		this.action = action;
	}
	
	public bool IsIdentical(Action<T1> action)
	{
		return this.action == action;
	}
	
	public void Invoke(T1 arg)
	{
		action(arg);
	}
}

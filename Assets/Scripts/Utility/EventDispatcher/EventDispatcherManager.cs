using UnityEngine;

public class EventDispatcherManager : MonoBehaviour
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void ResetDomain()
	{
		EventDispatcher.Reset();
	}
}
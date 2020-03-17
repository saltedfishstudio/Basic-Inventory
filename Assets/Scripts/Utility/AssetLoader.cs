using System;
using SFStudio.OpenWorld;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetLoader : MonoBehaviour
{

	public static GameObject itemFactory;
	public static bool initialized;
	
	public void Awake()
	{
		var handle = Addressables.LoadAssetAsync<GameObject>(ItemFactory.path);
		handle.Completed += o =>
		{
			itemFactory = o.Result;
			initialized = true;
		};
	}
}
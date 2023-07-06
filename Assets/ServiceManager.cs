using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    public static ServiceManager Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this.gameObject);

		Application.targetFrameRate = 60;
	}

	public DataManager dataManager;
	public IAPManager iapManager;
	public GameTimeManager gameTimeManager;
	public SoundManager soundManager;
	public AdsManager adsManager;
}

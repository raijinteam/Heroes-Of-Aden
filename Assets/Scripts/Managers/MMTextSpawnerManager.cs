using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class MMTextSpawnerManager : MonoBehaviour
{
	public static MMTextSpawnerManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	private MMF_Player mmf;
	private MMF_FloatingText floatingText;

	private void Start()
	{
		mmf = GetComponent<MMF_Player>();
		floatingText = mmf.GetFeedbackOfType<MMF_FloatingText>();
	}

	public void SpawnAtTarget(Transform _target, int _damageValue)
	{
		floatingText.TargetTransform = _target;
		floatingText.Value = _damageValue.ToString();
		floatingText.Play(_target.position);
	}

}

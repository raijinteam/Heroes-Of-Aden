using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLife : MonoBehaviour
{
    [SerializeField] private float flt_Time;

	private void Start()
	{
		Destroy(gameObject, flt_Time);
	}
}

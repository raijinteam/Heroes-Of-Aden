using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeDefenseMovement : MonoBehaviour
{
    [SerializeField] private float flt_RotateSpeed;

	public void SetSpeed(float _rotateSpeed)
	{
		flt_RotateSpeed = _rotateSpeed;
	}

	private void Update()
	{
		transform.Rotate(Vector3.forward * flt_RotateSpeed * Time.deltaTime);
	}
}

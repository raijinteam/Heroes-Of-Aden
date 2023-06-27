using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowBGRotation : MonoBehaviour
{

    [SerializeField] private float flt_RotationSpeed = -15f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, flt_RotationSpeed * Time.deltaTime));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVFXDestory : MonoBehaviour
{
    [SerializeField] private float timeForDestory = 2;

    private void OnEnable()
    {
        Destroy(this.gameObject, timeForDestory);
    }
}

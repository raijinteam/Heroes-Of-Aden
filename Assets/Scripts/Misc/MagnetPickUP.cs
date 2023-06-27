using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPickUP : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        SoundManager.Instance.PlayMagnetPickupSound();

        foreach(Transform point in GameManager.Instance.pointSpawnParent)
        {
            Debug.Log("Count of childern " + GameManager.Instance.pointSpawnParent.childCount);
            Debug.Log("name of : " + point.gameObject.name);
            point.GetComponentInChildren<Points>().ActiveMagnet();
        }

        foreach(Transform coin in GameManager.Instance.coinSpawnParent)
        {
            Debug.Log("Count of childern " + GameManager.Instance.coinSpawnParent.childCount);
            Debug.Log("name of : " + coin.gameObject.name);
            coin.GetComponentInChildren<CoinPickUp>().ActiveMagnet();
        }


        Destroy(gameObject);
    }
}

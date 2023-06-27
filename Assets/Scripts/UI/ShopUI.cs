using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{



    public void OnClick_BuyProduct(int index)
    {
        FindObjectOfType<IAPManager>().BuyConsumable(index);

    }
}

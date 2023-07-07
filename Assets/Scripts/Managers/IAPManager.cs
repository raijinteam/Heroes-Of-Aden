using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    //public static IAPManager Instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    
    public static string[] Products =
    {
        "com.armageddonstudio.heroesofaden.SpecialItem" ,
        "com.armageddonstudio.heroesofaden.GemsPack1" , "com.armageddonstudio.heroesofaden.GemsPack2" , "com.armageddonstudio.heroesofaden.GemsPack3" , "com.armageddonstudio.heroesofaden.GemsPack4" , "com.armageddonstudio.heroesofaden.GemsPack5"
    };

   

    //private void Awake()
    //{


    //    if (FindObjectsOfType(GetType()).Length > 1)
    //    {
    //        Destroy(gameObject);
    //    }

    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}

    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        for (int i = 0; i < Products.Length; i++)
        {
            builder.AddProduct(Products[i], ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyConsumable(int index)
    {
        BuyProductID(Products[index]);       
    }

    

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, Products[0], StringComparison.Ordinal))
        {
            //NO Ads Code Here
            // FindObjectOfType<AdsManager>().PurchasedNoAds();
            ServiceManager.Instance.dataManager.NoAds();
          //  UiManager.instance.uishop.noAdsPanel.SetActive(false);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[1], StringComparison.Ordinal))
        {
            //Special Item Code Here
          //  DataManager.instance.SetCoin(100);
            Debug.Log("Purchase Special Item");

            UIManager.Instance.ui_RewardSummary.SetRewardSummaryData(GameManager.Instance.list_SpecialItemIcons,GameManager.Instance.list_SpecialItemAmount);
            ServiceManager.Instance.dataManager.PurchaseSpecialItem();
            ServiceManager.Instance.dataManager.IncreaseCoins(UIManager.Instance.ui_Shop.specialItemCoinReward);
            ServiceManager.Instance.dataManager.IncreaseGems(UIManager.Instance.ui_Shop.specialItemGemsReward);
            ServiceManager.Instance.dataManager.UnlockedSpecialItemRewardPlayer(UIManager.Instance.ui_Shop.playerIndex);
            UIManager.Instance.ui_Shop.specialItemSection.SetActive(false);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[2], StringComparison.Ordinal))
        {
            ServiceManager.Instance.dataManager.IncreaseGems(50);
            Debug.Log("Add Gems Ads manager" + 50);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[3], StringComparison.Ordinal))
        {
            ServiceManager.Instance.dataManager.IncreaseGems(200);
            Debug.Log("Add Gems Ads manager" + 200);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[4], StringComparison.Ordinal)) {
            ServiceManager.Instance.dataManager.IncreaseGems(500);
            Debug.Log("Add Gems Ads manager" + 500);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[5], StringComparison.Ordinal))
        {
            ServiceManager.Instance.dataManager.IncreaseGems(1000);
            Debug.Log("Add Gems Ads manager" + 1000);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[6], StringComparison.Ordinal))
        {
            ServiceManager.Instance.dataManager.IncreaseGems(2500);
            Debug.Log("Add Gems Ads manager" + 2500);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message) {
  

    }
}

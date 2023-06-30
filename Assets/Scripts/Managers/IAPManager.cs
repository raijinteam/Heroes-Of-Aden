using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager Instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //public static string[] Products = { "com.parth.StairMaster.noads", "com.parth.StairMaster.coinpack1" , "com.parth.StairMaster.coinpack2"
    //      , "com.parth.StairMaster.coinpack3","com.parth.StairMaster.coinpack4"};

    public static string[] Products =
    {
        "com.shubham.HeroesofAden.NoAds" , "com.subham.HeroesofAden.SpecialItem" ,
        "com.shubham.HeroesofAden.GemsPack1" , "com.shubham.HeroesofAden.GemsPack2" , "com.shubham.HeroesofAden.GemsPack3" , "com.shubham.HeroesofAden.GemsPack4" , "com.shubham.HeroesofAden.GemsPack5" ,
         "com.shubham.HeroesofAden.EnergyPack1" , "com.shubham.HeroesofAden.EnergyPack2" ,
        "com.shubham.HeroesofAden.EnergyPack3" , "com.shubham.HeroesofAden.EnergyPack4" , "com.shubham.HeroesofAden.EnergyPack5"
    };


    private void Awake()
    {


        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

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
            DataManager.Instance.NoAds();
          //  UiManager.instance.uishop.noAdsPanel.SetActive(false);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[1], StringComparison.Ordinal))
        {
            //Special Item Code Here
          //  DataManager.instance.SetCoin(100);
            Debug.Log("Purchase Special Item");
            DataManager.Instance.PurchaseSpecialItem();
            DataManager.Instance.IncreaseCoins(UIManager.Instance.ui_Shop.specialItemCoinReward);
            DataManager.Instance.IncreaseGems(UIManager.Instance.ui_Shop.specialItemGemsReward);
            DataManager.Instance.UnlockedSpecialItemRewardPlayer(UIManager.Instance.ui_Shop.playerIndex);
            UIManager.Instance.ui_Shop.specialItemSection.SetActive(false);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[2], StringComparison.Ordinal))
        {
            DataManager.Instance.IncreaseGems(10);
            Debug.Log("Add Gems Ads manager" + 10);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[3], StringComparison.Ordinal))
        {
            DataManager.Instance.IncreaseGems(50);
            Debug.Log("Add Gems Ads manager" + 50);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[4], StringComparison.Ordinal)) {
            DataManager.Instance.IncreaseGems(100);
            Debug.Log("Add Gems Ads manager" + 100);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[5], StringComparison.Ordinal))
        {
            DataManager.Instance.IncreaseGems(500);
            Debug.Log("Add Gems Ads manager" + 500);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[6], StringComparison.Ordinal))
        {
            DataManager.Instance.IncreaseGems(1000);
            Debug.Log("Add Gems Ads manager" + 1000);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[7], StringComparison.Ordinal))
        {
            //Add Energy PAck 1
            DataManager.Instance.IncreaseEnergy(10);
            Debug.Log("Add Energy Ads manager" + 10);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[8], StringComparison.Ordinal))
        {
            //Add Energy Pack 2
            DataManager.Instance.IncreaseEnergy(50);
            Debug.Log("Add Energy Ads manager" + 50);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[9], StringComparison.Ordinal))
        {
            //Add Energy Pack 3
            DataManager.Instance.IncreaseEnergy(100);
            Debug.Log("Add Energy Ads manager" + 100);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[10], StringComparison.Ordinal))
        {
            //Add Energy Pack 4
            DataManager.Instance.IncreaseEnergy(500);
            Debug.Log("Add Energy Ads manager" + 500);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[11], StringComparison.Ordinal))
        {
            //Add Energy Pack 5
            DataManager.Instance.IncreaseEnergy(1000);
            Debug.Log("Add Energy Ads manager" + 1000);
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

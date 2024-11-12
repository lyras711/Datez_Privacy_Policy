using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Purchasing;

[Serializable]
public class NonConsumableItem
{
    public string Name;
    public string Id;
    public string description;
    public float price;
}


public class PurchasingManager : MonoBehaviour, IStoreListener
{
    IStoreController _storeController;
    public NonConsumableItem ncItem;
    [SerializeField] private Button[] unlockables;
    [SerializeField] private GameObject toDisable;

    private void Start()
    {
        SetUpBuilder();
    }

    public void Purchase()
    {
        // set playerprefs

        _storeController.InitiatePurchase(ncItem.Id);
    }

    void SetUpBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(ncItem.Id, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this,builder);
    }

    void CheckForPurchase(string id)
    {
        if (_storeController != null)
        {
            var product = _storeController.products.WithID(id);
            if (product != null)
            {
                if (product.hasReceipt)
                {
                    Unlock();
                }
            }
        }
    }

    void Unlock()
    {
        for (int i = 0; i < unlockables.Length; i++)
        {
            unlockables[i].interactable = true;
        }

        toDisable.SetActive(false);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Initilize Failed: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("Initilize Failed: " + error + message);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        Debug.Log("Purchase Complete: " + product.definition.id);

        if (product.definition.id == ncItem.Id)
        {
            Unlock();
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase Failed: " + failureReason);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Initialized");
        _storeController = controller;
        CheckForPurchase(ncItem.Id);
    }
}

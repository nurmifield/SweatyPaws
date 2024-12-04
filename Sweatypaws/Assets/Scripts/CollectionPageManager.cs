using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
public class CollectionPageData
{
    public string collectibles_image;
    public string[] collectibles_text;

    public CollectionPageData(string imageName , string[] collectiblesText)
    {
        this.collectibles_image = imageName;
        this.collectibles_text = collectiblesText;
    }
}
public class CollectionPageManager : MonoBehaviour
{
    public GameObject collectionCanvas;
    public GameObject nextButton;
    public GameObject previousButton;
    public TextMeshProUGUI collectionText;
    public UnityEngine.UI.Image collectionImage;
    public CollectionPageData collectionPageData;
    public GameObject collectionPanel;

    public void UpdateCollectionPage()
    {
        collectionCanvas.SetActive(true);
        collectionText.text = collectionPageData.collectibles_text[0];
        Sprite newCollectiblesImage = Resources.Load<Sprite>(collectionPageData.collectibles_image);
        collectionImage.sprite = newCollectiblesImage;
        if (collectionPageData.collectibles_text.Length > 0)
        {
            nextButton.SetActive(true);
        }
    }

    public void NextCollectionText()
    {
        collectionText.text = collectionPageData.collectibles_text[1];
        nextButton.SetActive(false);
        previousButton.SetActive(true);
    }

    public void PreviousCollectionText()
    {
        collectionText.text = collectionPageData.collectibles_text[0];
        nextButton.SetActive(true);
        previousButton.SetActive(false);
    }

    public void CloseCollection()
    {
        collectionCanvas.SetActive(false);
        collectionPanel.SetActive(true);
    }
}

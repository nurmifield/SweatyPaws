using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
public class CollectionPageData
{
    public string collectibles_image;
    public string collectibles_image_big;
    public string[] collectibles_text;

    public CollectionPageData(string imageName , string[] collectiblesText,string imageNameBig)
    {
        this.collectibles_image = imageName;
        this.collectibles_image_big = imageNameBig;
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
    public UnityEngine.UI.Image collectionImageBig;
    public CollectionPageData collectionPageData;
    public GameObject collectionPanel;
    public GameObject collectionPageBig;
    public int indexNumber = 0;

    public void UpdateCollectionPage()
    {
        collectionCanvas.SetActive(true);
        collectionText.text = collectionPageData.collectibles_text[indexNumber];
        Sprite newCollectiblesImage = Resources.Load<Sprite>(collectionPageData.collectibles_image);
        collectionImage.sprite = newCollectiblesImage;
        if (collectionPageData.collectibles_text.Length > 0)
        {
            nextButton.SetActive(true);
        }
    }

    public void NextCollectionText()
    {
        indexNumber++;
        collectionText.text = collectionPageData.collectibles_text[indexNumber];
        previousButton.SetActive(true);
        //Debug.Log(indexNumber);
        if (indexNumber == collectionPageData.collectibles_text.Length -1)
        {
            nextButton.SetActive(false);
        }
    }

    public void PreviousCollectionText()
    {
        indexNumber--;
        collectionText.text = collectionPageData.collectibles_text[indexNumber];
        nextButton.SetActive(true);
        //Debug.Log(indexNumber);
        if (indexNumber == 0)
        {
            previousButton.SetActive(false);
        }
    }

    public void CloseCollection()
    {
        collectionCanvas.SetActive(false);
        collectionPanel.SetActive(true);
        previousButton.SetActive(false);
        nextButton.SetActive(false);
        indexNumber = 0;
    }

    public void OpenPicture()
    {
        collectionPageBig.SetActive(true);
        Sprite newCollectiblesImage = Resources.Load<Sprite>(collectionPageData.collectibles_image_big);
        collectionImageBig.sprite = newCollectiblesImage;
    }

    public void ClosePicture()
    {
        collectionPageBig.SetActive(false);
    }
}

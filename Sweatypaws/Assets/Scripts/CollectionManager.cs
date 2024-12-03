using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public JsonReader jsonReader;
    public Transform buttonContainer;
    public List<GameObject> instatiatedPrefab;
    public GameObject prefab;
    public int indexNumber=0;
    public int collectionsNumber = 4;
    public int increaseNumber = 4;
    public GameObject nextButton;
    public GameObject previousButton;

    public void IncreaseIndexAndCollections()
    {
        this.indexNumber = indexNumber + increaseNumber;
        this.collectionsNumber = collectionsNumber + increaseNumber;
    }
    public void DecreaseIndexAndCollections()
    {
        this.indexNumber = indexNumber - increaseNumber;
        this.collectionsNumber = collectionsNumber - increaseNumber;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject reader = GameObject.Find("Reader");
        if (reader != null)
        {
            jsonReader = reader.GetComponent<JsonReader>();

            if (jsonReader != null)
            {
               
                for (int i=0; i< 4;i++) 
                { 
                  GameObject newButton = Instantiate(prefab,buttonContainer);
                  instatiatedPrefab.Add(newButton);
                    newButton.SetActive(false);
                }
                if (jsonReader.collectionsData.collectibles.Length <= 4)
                {
                    for (int i = 0; i < jsonReader.collectionsData.collectibles.Length; i++)
                    {
                        string imageName = jsonReader.collectionsData.collectibles[i].level_collectible.collectibles_image;
                        string name = jsonReader.collectionsData.collectibles[i].level_name;
                        ButtonImageAndName(i, name, imageName);
                        instatiatedPrefab[i].SetActive(true);
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        string imageName = jsonReader.collectionsData.collectibles[i].level_collectible.collectibles_image;
                        string name = jsonReader.collectionsData.collectibles[i].level_name;
                        ButtonImageAndName(i, name, imageName);
                        instatiatedPrefab[i].SetActive(true);
                        // buttonin näkyviin laitto tänne next
                        nextButton.SetActive(true);
                    }
                }
               

            }
        }
    }

    public void ButtonImageAndName(int index, string name , string image)
    {
        
        var player = PlayerManager.Instance;
        instatiatedPrefab[index].name = name;
        Sprite newCharacterSprite = Resources.Load<Sprite>(image);
        UnityEngine.UI.Image buttonImage = instatiatedPrefab[index].GetComponent<UnityEngine.UI.Image>();
        buttonImage.sprite = newCharacterSprite;
        for (int i = 0; i < player.playerData.level_progress.Count;i++)
        {
            if (player.playerData.level_progress[i].level_name == name)
            {
                if (player.playerData.level_progress[i].complete)
                {
                    buttonImage.color = Color.white; 
                    break;
                }
                else
                {
                    buttonImage.color = Color.black;
                    break;
                }
            }
        }
       

    }
    public void NextCollectionsUpdate()
    {
        IncreaseIndexAndCollections();

        if (indexNumber >= 4)
        {
            // buttonin laitto tänne previous
            previousButton.SetActive(true);
        }
        int counter = 0;
        if (collectionsNumber < jsonReader.collectionsData.collectibles.Length)
        {
            for (int i = indexNumber; i < collectionsNumber; i++)
            {
                
                string imageName = jsonReader.collectionsData.collectibles[i].level_collectible.collectibles_image;
                string name = jsonReader.collectionsData.collectibles[i].level_name;
                ButtonImageAndName(counter,name,imageName);
                counter++;
            }
        }
        else
        {
            for (int i = indexNumber; i < jsonReader.collectionsData.collectibles.Length; i++)
            {
                string imageName = jsonReader.collectionsData.collectibles[i].level_collectible.collectibles_image;
                string name = jsonReader.collectionsData.collectibles[i].level_name;
                ButtonImageAndName(counter, name, imageName);
                counter++ ;
                // buttonin kadotus tänne next 
                nextButton.SetActive(false);
            }
            if (counter < 4)
            {
                for(int i = counter; i< 4; i++)
                {
                    instatiatedPrefab[i].SetActive(false);
                }
            }
        }
        
    }

    public void PreviousCollectionsUpdate()
    {
        DecreaseIndexAndCollections();
        if (indexNumber == 0)
        {
            // buttonin kadotus tänne previous
            previousButton.SetActive(false);
        }
        if (!nextButton.activeSelf)
        {
            nextButton.SetActive(true);
        }
            int counter = 0;
            for (int i = indexNumber; i < collectionsNumber; i++)
            {
                string imageName = jsonReader.collectionsData.collectibles[i].level_collectible.collectibles_image;
                string name = jsonReader.collectionsData.collectibles[i].level_name;
                ButtonImageAndName(counter, name, imageName);
            if (!instatiatedPrefab[counter].activeSelf)
            {
                instatiatedPrefab[counter].SetActive(true);
            }
            counter++ ;
            }
        

    }
}

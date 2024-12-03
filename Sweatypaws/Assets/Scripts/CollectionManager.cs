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
   
                for (int i = 0; i < jsonReader.collectionsData.collectibles.Length; i++)
                {
                    string imageName = jsonReader.collectionsData.collectibles[i].level_collectible.collectibles_image;
                    string name = jsonReader.collectionsData.collectibles[i].level_name;
                    ButtonImageAndName(i, name, imageName);
                    instatiatedPrefab[i].SetActive(true);
                }

            }
        }
    }

    public void ButtonImageAndName(int index, string name , string image)
    {
        instatiatedPrefab[index].name = name;
        Sprite newCharacterSprite = Resources.Load<Sprite>(image);
        instatiatedPrefab[index].GetComponent<UnityEngine.UI.Image>().sprite=newCharacterSprite;

    }

}

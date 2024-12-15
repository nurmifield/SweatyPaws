using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public GameObject endScreenObject;
    public GameObject prefab;
    public GameObject newEndScreen;
    // Start is called before the first frame update
    void Start()
    {
        var player = PlayerManager.Instance;
        if (player.playerData.level==3)
        {
            endScreenObject.SetActive(true);
        }
    }
    public void OpenEndScreenAnimation()
    {
        GameObject prefabObject= Instantiate(prefab);
        newEndScreen = prefabObject;
      

    }
    public void DestroyEndScreen()
    {
        Destroy(newEndScreen);
    }


}

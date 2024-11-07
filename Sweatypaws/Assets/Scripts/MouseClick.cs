using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour
{
    public GameObject menuPanel;
    GameObject obj;
    private void Start()
    {
        menuPanel.SetActive(false);
        
    }
    void Update()
    {
        //If the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            obj = GameObject.FindWithTag("MenuPanel");
            if (obj != null)
            {
                if (obj.activeSelf)
                {
                    Debug.Log("Paneeli on aktiivinen");
                    return;
                }
            }
           
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
                GetComponent<CheckCorrectTool>().SetAction(hit.collider.gameObject);
                
            }
        }
    }
}

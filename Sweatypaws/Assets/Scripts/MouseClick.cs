using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour
{
    public GameObject menuPanel;
    GameObject obj;
    public Vector2 startPos;
    public Vector2 endPos;
    public Vector2 direction;
    public bool swiping;
    public bool tap;

    private void Start()
    {
        menuPanel.SetActive(false);
        
    }
    void Update()
    {
        if (Input.touchCount == 1 && !CheckUiPanel())
        {

            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    Debug.Log("Kosketus alkoi");
                    break;
                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    endPos = touch.position;
                    Debug.Log("Kosketus loppui");
                    if (startPos == endPos)
                    {
                        tap=true;
                    }
                    if (startPos != endPos)
                    {
                        swiping = true;
                        
                        
                    }
                   
                    break;
            }


            if (swiping && GetComponent<Player>().tool == "pihdit")
            {
                swiping = false;
                Vector2 worldPointStart = Camera.main.ScreenToWorldPoint(startPos);
                Vector2 worldPointEnd = Camera.main.ScreenToWorldPoint(endPos);
                float distance = Vector2.Distance(worldPointStart, worldPointEnd);
                Vector2 direction = (worldPointEnd - worldPointStart).normalized;
                RaycastHit2D hit = Physics2D.Raycast(worldPointStart, direction,distance);

                if (hit.collider != null)
                {
                    //GetComponent<CheckCorrectTool>().SetAction(hit.collider.gameObject);
                    GetComponent<GameActionManager>().CheckStagePart(hit.collider.gameObject);
                    //Debug.Log(hit.collider.gameObject);

                }


            }
            if (tap && GetComponent<Player>().tool != "pihdit" && GetComponent<Player>().tool != "none")
            {
                tap = false;
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                //If something was hit, the RaycastHit2D.collider will not be null.
                if (hit.collider != null)
                {
                    //GetComponent<CheckCorrectTool>().SetAction(hit.collider.gameObject);
                    GetComponent<GameActionManager>().CheckStagePart(hit.collider.gameObject);

                }
            }

        }
    
        /*
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
                //GetComponent<CheckCorrectTool>().SetAction(hit.collider.gameObject);
                GetComponent<GameActionManager>().CheckStagePart(hit.collider.gameObject);
                
            }
        }*/
    }
    public bool CheckUiPanel()
    {
        bool uiPanelActive = false;
        obj = GameObject.FindWithTag("MenuPanel");
        if (obj != null)
        {
            if (obj.activeSelf)
            {
                Debug.Log("Paneeli on aktiivinen");

                uiPanelActive = true;
            }
        }
        return uiPanelActive;
    }
}

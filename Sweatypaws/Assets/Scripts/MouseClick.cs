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
    public AudioSource audioSource;
    public AudioClip [] clips;

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
                    //Debug.Log("Kosketus alkoi");
                    break;
                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    endPos = touch.position;
                    //Debug.Log("Kosketus loppui");
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
                //RaycastHit2D hit = Physics2D.Raycast(worldPointStart, direction,distance);
                /* VANHA MALLI
                if (hit.collider != null)
                {
                    //GetComponent<CheckCorrectTool>().SetAction(hit.collider.gameObject);
                    GetComponent<GameActionManager>().CheckStagePart(hit.collider.gameObject);
                    //Debug.Log(hit.collider.gameObject);

                }*/
                string[] layerOrder = { "First", "Second", "Third","Fourth", "Fifth" };

                GameObject topObject = null;

                // Iterate over the layers in order of priority
                foreach (string layer in layerOrder)
                {
                    // Create a LayerMask for the current layer
                    int layerMask = LayerMask.GetMask(layer);

                    // Perform a raycast from the current world position, with the layerMask to filter layers
                    RaycastHit2D hit = Physics2D.Raycast(worldPointStart, direction, distance,layerMask);

                    if (hit.collider != null)
                    {
                        // If an object is hit, set the topObject and stop checking further layers
                        topObject = hit.collider.gameObject;
                        break; // Exit the loop as we found an object in the highest priority layer
                    }
                }

                // If we found a top object, perform interaction logic
                if (topObject != null)
                {
                    //Debug.Log("Touched object: " + topObject.name);
                    // Perform interaction logic here
                    PlaySound(GetComponent<Player>().tool);
                    GetComponent<GameActionManager>().CheckStagePart(topObject);
                }


            }
            if (tap && GetComponent<Player>().tool != "pihdit" && GetComponent<Player>().tool != "none")
            {
                tap = false;
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                /* VANHA MALLI
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                //If something was hit, the RaycastHit2D.collider will not be null.
                if (hit.collider != null)
                {
                    //GetComponent<CheckCorrectTool>().SetAction(hit.collider.gameObject);
                    GetComponent<GameActionManager>().CheckStagePart(hit.collider.gameObject);

                }*/
                string[] layerOrder = { "First", "Second", "Third", "Fourth", "Fifth" };

                GameObject topObject = null;

                // Iterate over the layers in order of priority
                foreach (string layer in layerOrder)
                {
                    // Create a LayerMask for the current layer
                    int layerMask = LayerMask.GetMask(layer);

                    // Perform a raycast from the current world position, with the layerMask to filter layers
                    RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, layerMask);

                    if (hit.collider != null)
                    {
                        // If an object is hit, set the topObject and stop checking further layers
                        topObject = hit.collider.gameObject;
                        break; // Exit the loop as we found an object in the highest priority layer
                    }
                }

                // If we found a top object, perform interaction logic
                if (topObject != null)
                {
                    //Debug.Log("Touched object: " + topObject.name);
                    // Perform interaction logic here
                    PlaySound(GetComponent<Player>().tool);
                    GetComponent<GameActionManager>().CheckStagePart(topObject);
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
    public void PlaySound(string toolName)
    {
        int audioindex=-1;
        if (toolName == "pihdit"){
            audioindex = 0;
        }
        else if (toolName == "Screwdriver"){
            audioindex = 1;
        }
        else if (toolName == "Tweezer"){
            audioindex = 2;
        }
        else if (toolName == "Liquid Nitrogen"){
            audioindex = 3;
        }
        else if (toolName == "Magnet"){
            audioindex = 4;
        }
        if (audioindex != -1){
            audioSource.PlayOneShot(clips[audioindex]);
        }
    }
    public bool CheckUiPanel()
    {
        bool uiPanelActive = false;
        obj = GameObject.FindWithTag("MenuPanel");
        if (obj != null)
        {
            if (obj.activeSelf)
            {
                //Debug.Log("Paneeli on aktiivinen");

                uiPanelActive = true;
            }
        }
        return uiPanelActive;
    }
}

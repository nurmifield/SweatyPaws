using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueList : MonoBehaviour
{
    public Dialogue[] dialogue;
    private JsonReader data;

    private void Start()
    {
       data=FindObjectOfType<JsonReader>();

        
    }


}

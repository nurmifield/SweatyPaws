using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CollectionsData
{
    public Collection[] collectibles;
    [System.Serializable]
    public class Collection
    {
        public string level_name;
        public CollectionData level_collectible;
    }
    [System.Serializable]
    public class CollectionData
    {
        public string collectibles_image;
        public string[] collectibles_text;
    }
}

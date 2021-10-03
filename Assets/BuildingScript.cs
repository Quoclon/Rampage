using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public List<GameObject> blocks;
    public List<BlockScript> blockScripts;


    // Start is called before the first frame update
    void Start()
    {
        foreach(BlockScript blockScript in GetComponentsInChildren<BlockScript>())
        {
            //Debug.Log(blockScript.gameObject.name);
            blockScripts.Add(blockScript);
            blocks.Add(blockScript.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlockUpdate()
    {

    }
}

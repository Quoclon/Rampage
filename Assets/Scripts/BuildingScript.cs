using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public List<GameObject> blocks;
    public List<BlockScript> blockScripts;

    public int brokenBlocks;


    // Start is called before the first frame update
    void Start()
    {
        brokenBlocks = 0;

        foreach(BlockScript blockScript in GetComponentsInChildren<BlockScript>())
        {
            //Debug.Log(blockScript.gameObject.name);
            blockScripts.Add(blockScript);
            blocks.Add(blockScript.gameObject);
        }

        Debug.Log(blocks.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakBlock()
    {
        brokenBlocks++;
        //Debug.Log(brokenBlocks);

        if(brokenBlocks >= blocks.Count / 2)
        {
            Debug.Log("Destroyed Building");
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public BuildingScript buildingScript;

    public bool isTopBlock;
    public bool isBreakable;
    public bool isBroken;
    public bool isCountedInDestruction;

    public GameObject windowClosed;
    public GameObject windowOpen;
    public GameObject Brick;
    public GameObject BrickBroken;
    public GameObject roof;

    // Start is called before the first frame update
    void Start()
    {
        buildingScript = GetComponentInParent<BuildingScript>();
        if (isTopBlock)
            roof.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakBlocks()
    {
        if (isBroken)
            return;

        //Adjust Sprites
        windowOpen.SetActive(false);
        windowClosed.SetActive(false);
        Brick.SetActive(false);
        BrickBroken.SetActive(true);

        isBroken = true;

        //Break Block at Building Level
        buildingScript.BreakBlock();
    }
}

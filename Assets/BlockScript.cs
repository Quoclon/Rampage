using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public bool isTopBlock;
    public bool isBreakable;
    public bool isCountedInDestruction;

    public GameObject windowClosed;
    public GameObject windowOpen;
    public GameObject Brick;
    public GameObject BrickBroken;

    public int blockState;

    public GameObject roof;

    // Start is called before the first frame update
    void Start()
    {
        blockState = 0;
        if (isTopBlock)
            roof.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

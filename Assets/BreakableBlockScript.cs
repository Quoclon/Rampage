using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlockScript : MonoBehaviour
{
    public BlockScript blockScript;

    void Start()
    {
        blockScript = GetComponentInParent<BlockScript>();
    }

    public void BreakBrick()
    {
        blockScript.BreakBlocks();
    }
}

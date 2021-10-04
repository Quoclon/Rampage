using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public BuildingScript buildingScript;

    [Header("States")]
    public bool isTopBlock;
    public bool isBroken;

    [Header("Brick Options")]
    public bool canSpawnEnemies;

    [Header("Unused Variables")]
    public bool isBreakable;
    public bool isCountedInDestruction;

    [Header("Sprites")]
    public GameObject windowClosed;
    public GameObject windowOpen;
    public GameObject Brick;
    public GameObject BrickBroken;
    public GameObject roof;

    [Header("Spawnable Items")]
    //Array of Items to Spawn on Broken
    public GameObject[] spawnableItem;

    [Header("Spawnable Enemies")]
    //Array of Items to Spawn on Broken
    public GameObject[] spawnableEnemy;
    public bool isEnemySpawned;


    // Start is called before the first frame update
    void Start()
    {
        //canSpawnEnemies
        canSpawnEnemies = Random.Range(0, 3) == 0 ? true : false;
        Debug.Log(canSpawnEnemies);

        //Timer Setup
        ResetTimerWithRandomRange();

        //Get reference to the BuildingScript
        buildingScript = GetComponentInParent<BuildingScript>();

        //Setup the Roof Sprite/Prefab if this is the top of the building
        if (isTopBlock)
            roof.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimer();
    }

    public void SpawnEnemy()
    {
        //After Countdown, random chance to spawn enemy
        int randomChance = Random.Range(0, 6);

        if(randomChance == 0 && canSpawnEnemies && !isBroken)
        {
            EnemySpawnBlockUpdate();

            //Spawn Random Game Object
            int randomItem = Random.Range(0, spawnableEnemy.Length);
            Instantiate(spawnableEnemy[Random.Range(0, spawnableEnemy.Length)], transform);
            isEnemySpawned = true;
        }
    }




    #region BlockChanges
    void EnemySpawnBlockUpdate()
    {
        //Deactivate Sprites
        BrickBroken.SetActive(false);
        windowClosed.SetActive(false);

        //Activate Sprites
        Brick.SetActive(true);
        windowOpen.SetActive(true);
    }

    public void KillEnemy()
    {

    }


    //Typically Called from the PunchScript - which has the collider
    public void BreakBlocks()
    {
        if (isBroken)
            return;

        //Deactivate Sprites
        windowOpen.SetActive(false);
        windowClosed.SetActive(false);

        //Activate Sprites
        Brick.SetActive(true);
        BrickBroken.SetActive(true);

        //Set Brick State
        isBroken = true;

        //Inform Building Script Block broken (calculate total building dmg)
        buildingScript.BreakBlock();

        //Spawn Random Game Object
        //~Check if building destroyed, if not do not spawn
        int randomItem = Random.Range(0, spawnableItem.Length + 1);
        if (randomItem < spawnableItem.Length)
            Instantiate(spawnableItem[Random.Range(0, spawnableItem.Length)], transform);
    }
    #endregion


    #region Timer Stuff
    //Variables
    [Header("Timer Stuff")]
    public float currentTime;
    public float targetTime;
    public float randomTimeAdjuster;

    private void HandleTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0.25f)
            TimerEnded();
    }

    void TimerEnded()
    {
        SpawnEnemy();
        ResetTimerWithRandomRange();
        //ResetTimer();
    }

    void ResetTimer()
    {
        currentTime = targetTime;
    }

    void ResetTimerWithRandomRange()
    {
        currentTime = targetTime + UnityEngine.Random.Range((-randomTimeAdjuster), (randomTimeAdjuster));
    }
    #endregion

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimerWithRandomRange();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimer();
    }


    private void FireWeapon()
    {
        Instantiate(projectile, shootPoint.position, Quaternion.identity);
        ResetTimerWithRandomRange();
    }


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
        FireWeapon();
        ResetTimerWithRandomRange();
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

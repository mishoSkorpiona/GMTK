using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceCrystal : MonoBehaviour
{
    [SerializeField] private float _timeUntillRoll = 30;
    [SerializeField] private GameObject dice;

    public int diceSide = 0; // 0,1,2,3

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
        Effect();
    }

    void CountTime()
    {
        _timeUntillRoll -= Time.deltaTime;
    }

    void Effect()
    {
        if (_timeUntillRoll < 5 && _timeUntillRoll > 1)
        {
            // pay system
        }
        else if (_timeUntillRoll < 1)
        {
            Roll();
        }
    }

    void Roll()
    {
        Vector3 randomRotation = default;
        float rot = 720*3;

        randomRotation.x = Random.Range(0, rot);
        rot -= randomRotation.x;
        randomRotation.y = Random.Range(0, rot);
        rot -= randomRotation.y;
        randomRotation.z = rot;

        LeanTween.rotate(dice, randomRotation, 1);
        diceSide = Random.Range(0, 4);
        Debug.Log(diceSide);
        _timeUntillRoll = 10;
    }
}
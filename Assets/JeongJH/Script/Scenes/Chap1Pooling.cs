using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Chap1Pooling : MonoBehaviour
{
    [SerializeField] PooledObject FirePrefab;
    [SerializeField] PooledObject smallFirePrefab;
    [SerializeField] PooledObject lightningPrefab;
    [SerializeField] PooledObject dangerCircle;
    [SerializeField] int size;
    [SerializeField] int capacity;
    [SerializeField] int smallSize;
    [SerializeField] int smallCapacity;


    private void Start()
    {
        /*Manager.Pool.CreatePool(FirePrefab, size, capacity);
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);
        Manager.Pool.CreatePool(lightningPrefab, size, capacity);
        Manager.Pool.CreatePool(dangerCircle, size, capacity);*/
    }
}

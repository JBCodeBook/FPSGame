using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] private GameObject[] avaliableCovers;
    [SerializeField] private Transform[] coverSpots;
    
    public void Awake()
    {
        // Finds location of all Cover in scene and stores their transform
        int i = 0;
        avaliableCovers = GameObject.FindGameObjectsWithTag("Cover");
        if (avaliableCovers != null)
        {
/*            foreach (GameObject item in avaliableCovers)
            {
                coverSpots[i++] = item.transform;
            }*/
        }
        else
        {
            Debug.Log("No Cover Locations Assigned");
        }
    }
    
    public Transform[] GetCoverSpots()
    {
        return coverSpots;
    }
}

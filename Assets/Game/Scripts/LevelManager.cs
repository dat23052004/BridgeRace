using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


public class LevelManager : MonoBehaviour
{    

    public Brick brick;

    [SerializeField] private int gridX = 10;
    [SerializeField] private int gridZ = 10;
    [SerializeField] private float gridSpacingOffset = 2f;
    public List<Brick> brickList = new List<Brick>();
    
    // Start is called before the first frame update
    void Start()
    {
        LoadMap();
    }

    // Update is called once per frame
    void Update()
    {

    }    

    public void LoadMap()
    {       
        Vector3 startPos = transform.position - new Vector3(gridX * .5f * gridSpacingOffset, 0, gridZ * .5f * gridSpacingOffset);

        for (int x = 0; x <= gridX; x++)
        {
            for (int z = 0; z <= gridZ; z++)
            {
                
                Vector3 spawnPos = startPos + new Vector3(x * gridSpacingOffset, .2f, z * gridSpacingOffset);
                Brick newBrick = Instantiate(brick, spawnPos, Quaternion.identity);
                brickList.Add(newBrick);
                //brick.transform.SetParent(transform);
  
            }
        }
        //Debug.Log(brickList.Count);
    }   



}





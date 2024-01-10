using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : ColorObject
{
    


    private void Start()
    {
        ChangeColor((ColorType)Random.Range(0, 4));
    }

    private void Update()
    {

    }

}
    //public GameObject brick;
    //public MeshRenderer render;
    //public Material[] materials;
    //public Player player;

    //private List<Brick> collidedBricks = new List<Brick>();
    ////private bool canSpawn = true;
    //private void Start()
    //{
    //    SetRandomColor();
    //}

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Player") )
    //    {

    //        other.GetComponent<Player>().AddBrick();
    //        collidedBricks.Add(this);
    //        StartCoroutine(SpawnBrick());
    //    }


    //}

    //private IEnumerator SpawnBrick()
    //{

    //    brick.SetActive(false);

    //    Debug.Log("Brick set to inactive");

    //    yield return new WaitForSeconds(4f);

    //    if (brick != null)
    //    {
    //        brick.SetActive(true);
    //        Debug.Log("Brick set to active after delay");
    //    }
    //    else
    //    {
    //        Debug.Log("Brick is null");
    //    }

    //    foreach (Brick collidedBrick in collidedBricks)
    //    {
    //        collidedBrick.SpawnWithOriginalColor();
    //    }

    //    collidedBricks.Clear();
    //    //canSpawn = true;
    //}
    //public void SpawnWithOriginalColor()
    //{
    //    brick.SetActive(true);
    //    SetRandomColor();
    //}
    //public void SetRandomColor()
    //{
    //    if (materials.Length > 0)
    //    {
    //        int randomIndex = Random.Range(0, materials.Length);
    //        render.material = materials[randomIndex];
    //    }
    //    else
    //    {
    //        Debug.LogError("No materials assigned to the player.");
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    Player otherPlayer = other.GetComponent<Player>();
    //    if (other.CompareTag("Player") && otherPlayer != null && otherPlayer.render != null)
    //    {
    //        Material playerMaterial = otherPlayer.render.material;
    //        if (render.material == playerMaterial)
    //        {
    //            brick.SetActive(false);
    //            otherPlayer.AddBrick();
    //            Debug.Log(1);
    //        }
    //    }
    //}

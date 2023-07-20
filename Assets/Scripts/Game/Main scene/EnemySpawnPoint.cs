using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public List<Collider2D> spawnPointCollider;
    public SpriteRenderer promptSprite { get; set; }
    public static EnemySpawnPoint Instance { get; private set; }

    //public GameObject spawnedObject;
    private void Awake()
    {
        //spawnedObject = transform.Find("SpawnObject").gameObject;
        Instance = this;
        for(int i = 0; i < GetComponentsInChildren<Collider2D>().Length; i++)
        {
            spawnPointCollider.Add(GetComponentsInChildren<Collider2D>()[i]);
        }
       
        promptSprite = this.transform.Find("prompt").GetComponent<SpriteRenderer>();
        //print(spawnPointCollider.Count);
        //spawnPointCollider.isTrigger = true;
    }
}

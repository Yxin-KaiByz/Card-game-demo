using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public Collider2D spawnPointCollider { get; private set; }
    public SpriteRenderer promptSprite { get; set; }
    public static EnemySpawnPoint Instance { get; private set; }

    //public GameObject spawnedObject;
    private void Awake()
    {
        //spawnedObject = transform.Find("SpawnObject").gameObject;
        Instance = this;
        spawnPointCollider = GetComponent<Collider2D>();
        promptSprite = this.transform.Find("prompt").GetComponent<SpriteRenderer>();
        
        spawnPointCollider.isTrigger = true;
    }
}

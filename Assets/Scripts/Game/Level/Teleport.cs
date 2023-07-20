using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    public Collider2D collider2d;
    public Collider2D playerCollider2d;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        collider2d = gameObject.GetComponent<Collider2D>();
        playerCollider2d = player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collider2d.IsTouching(playerCollider2d))
        {
            

            print("is touching with player");
            MainLevelManager.Instance.changeToBossBackground();
            PlayerObject.currentLevel = "Boss";
            Destroy(this.gameObject);
            
        }
    }
}

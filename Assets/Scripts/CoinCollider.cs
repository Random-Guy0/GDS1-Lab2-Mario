using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinCollider : MonoBehaviour
{
    Tilemap tilemap;
    Vector3Int pPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pPos = tilemap.WorldToCell(collision.rigidbody.position);
            Debug.Log("pPos:" + pPos);
            tilemap.SetTile(pPos, null);
        }
    }
}

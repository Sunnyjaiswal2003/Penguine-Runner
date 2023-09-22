using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunks : MonoBehaviour
{
    public float chunckLength;
    public float speed = -5;
    bool running;
   public Transform lastpoint;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        running = true;
        player = FindObjectOfType<PlayerController>().transform;
    }

    public Chunks showChunk()
    {
        gameObject.SetActive(true);
        return this;
    }

    public void StopChunk()
    {
        speed = 0;
    }
    private void FixedUpdate()
    {
        if (running)
        {
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if(player)
        if (player.position.z > lastpoint.position.z + 20)
        {
            Destroy(this.gameObject);
        }
    }
    

}

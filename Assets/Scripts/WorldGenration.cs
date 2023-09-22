using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenration : MonoBehaviour
{
    public GameObject groundPreFab;
    public float spawnDelay = 1f;
    public float spawnDistance = 10f;
    float spawnTimer;
    public int GroundCount = 1;
    Transform LastPoint;
    [SerializeField] private List<GameObject> chunkPreFab;
    
    public GameObject player;
    public bool DontConsiderFirstPrefab;

    float timer;
    public GameObject giftprefab;

    private void Awake()
    {
        if (!DontConsiderFirstPrefab) { SpawnNewChunk(0, true); }
        SpawnNewChunk(4);
    }

    private void Start()
    {
        if (chunkPreFab.Count == 0)
        {
            return;
        }
       
    }

    private void Update()
    {
        if(LastPoint)
        if (Vector3.Distance(player.transform.position, LastPoint.position) < 40)
        {
            SpawnNewChunk(5);
        }

        timer += Time.deltaTime;

        if (timer > 10)
        {
            timer = 0;

           
            if (!DontConsiderFirstPrefab)
            {
                var Giftpoints = GameObject.FindGameObjectsWithTag("Gift");

                List<GameObject> gifts = new List<GameObject>();
                foreach (var g in Giftpoints)
                {
                    if (g.transform.position.z > player.transform.position.z)
                    {
                        gifts.Add(g);
                    }
                }

                var giftpoints = gifts[Random.Range(0, gifts.Count - 1)];
                Instantiate(giftprefab, giftpoints.transform.position, Quaternion.identity, giftpoints.transform);
            }
        }
    }

    private void SpawnNewChunk(int Size, bool first = false)
    {
        if (first)
        {
            var go = Instantiate(groundPreFab, Vector3.zero, Quaternion.identity, transform);
            var chunk = go.GetComponent<Chunks>();
            LastPoint = chunk.lastpoint;
            
        }
        for(int i=0; i<Size; i++)
        {
            int RendomIndex = Random.Range(0, chunkPreFab.Count);
            
            if (!LastPoint)
            {
                var go = Instantiate(chunkPreFab[RendomIndex], Vector3.zero, Quaternion.identity, transform);
                var chunk = go.GetComponent<Chunks>();
                LastPoint = chunk.lastpoint;
            }
            else
            {
                var go = Instantiate(chunkPreFab[RendomIndex], LastPoint.position, Quaternion.identity, transform);
                var chunk = go.GetComponent<Chunks>();
                LastPoint = chunk.lastpoint;
            }

            
        } 
    }
}

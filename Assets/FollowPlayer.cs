using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public Vector3 Offset;
    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position+Offset, 1 * Time.deltaTime);
        
    }
}

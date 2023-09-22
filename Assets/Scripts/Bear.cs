using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public Transform Player;
    public Vector3 OffSet;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position + OffSet, Time.deltaTime);
    }

    public void Hunt(Transform Pos)
    {
        Player = Pos;
        Invoke(nameof(DelayHunt), 1.4f);
    }

    void DelayHunt()
    {
        OffSet = new Vector3(0, .2f, -1.0f);
        Invoke(nameof(EatAnim),1.5f);

    }

    void EatAnim()
    {
        this.transform.rotation = Quaternion.Euler(0,-44,0);
        anim.SetTrigger("Eat");
    }
}

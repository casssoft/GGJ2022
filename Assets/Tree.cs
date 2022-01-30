using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool aimLeft;
    public float delayBetweenTreeBarks = 4;
    float lastBark;
    public GameObject sibling;
    public GameObject barkPrefab;
    public GameObject tre;
    // Start is called before the first frame update
    void Start()
    {
        lastBark = Time.time;
        sibling = GameObject.Find("Sibling");
    }

    // Update is called once per frame
    void Update()
    {
        // tree barks on fixed interval
        if (GlobalVariables.followPlayer) {       
            if ((tre.transform.position - sibling.transform.position).magnitude < 8) {
                Bark();
            }
        }
        
        
    }

    void Bark(){
        if (Time.time - lastBark > delayBetweenTreeBarks) {
            GameObject bark = Instantiate(barkPrefab, tre.transform.position, this.gameObject.transform.rotation);
            lastBark = Time.time;
        }
    }
}

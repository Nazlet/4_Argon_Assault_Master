using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player1":
                print("Player1");
                break;
            case "Player2":
                print("Player2");
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }
}

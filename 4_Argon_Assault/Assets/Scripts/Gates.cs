using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour

{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;

    void OnTriggerEnter(Collider other)
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
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

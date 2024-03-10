using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float speed = 1.0f;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, speed);
        transform.rotation *= new Quaternion(.5f, .5f, .5f, 1f);
        if (transform.position.z > 30)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Destroy(other.gameObject);
        Destroy(gameObject);
        if(other.tag == "Fox")
        {
            GameManager.point += 20;
        }
        else
        {
            GameManager.point += 10;
        }
    }
}

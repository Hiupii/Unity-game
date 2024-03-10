using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Animal : MonoBehaviour
{

    [SerializeField] private float speed = 0.5f;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 0, speed);
        if(transform.position.z < -25)
        {
            Destroy(gameObject);
            gameManager.hp--;
        }
    }
}

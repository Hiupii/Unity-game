using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float horizontalInput;
    public GameObject bone;

    private float minX = -13f;
    private float maxX = 13f;

    [SerializeField] private Animator playerAnim;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        playerAnim.SetBool("Static_b", false);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Movements
        if (horizontalInput != 0)
        {
            transform.Translate(new Vector3(horizontalInput * speed, 0, 0));
            playerAnim.SetFloat("Speed_f", 0.3f);
        }
        else
            playerAnim.SetFloat("Speed_f", 0);

        // Throw bones
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shoot");
            Instantiate(bone, transform.position + new Vector3(0, 1, 0), transform.rotation, gameManager.inGameObject.transform);
//            playerAnim.SetInteger("Animation_int", 10);
        }

        //Jump for avoid Object

        // Prevent player out of Bound
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
        transform.position = currentPosition;
    }
}

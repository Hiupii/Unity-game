using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]private List<GameObject> hps;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        for (int i = 0; i < 5; i++)
        {
            hps[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameManager.hp)
        {
            case 5:
                hps[0].SetActive(true);
                hps[1].SetActive(true);
                hps[2].SetActive(true);
                hps[3].SetActive(true);
                hps[4].SetActive(true);
                break;
            case 4:
                hps[0].SetActive(true);
                hps[1].SetActive(true);
                hps[2].SetActive(true);
                hps[3].SetActive(true);
                hps[4].SetActive(false);
                break;
            case 3:
                hps[0].SetActive(true);
                hps[1].SetActive(true);
                hps[2].SetActive(true);
                hps[3].SetActive(false);
                hps[4].SetActive(false);
                break;
            case 2:
                hps[0].SetActive(true);
                hps[1].SetActive(true);
                hps[2].SetActive(false);
                hps[3].SetActive(false);
                hps[4].SetActive(false);
                break;
            case 1:
                hps[0].SetActive(true);
                hps[1].SetActive(false);
                hps[2].SetActive(false);
                hps[3].SetActive(false);
                hps[4].SetActive(false);
                break;
            case 0:
                hps[0].SetActive(false);
                hps[1].SetActive(false);
                hps[2].SetActive(false);
                hps[3].SetActive(false);
                hps[4].SetActive(false);
                break;
        }    
    }
}

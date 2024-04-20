using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> animals;

    private bool wait;
    private float minPosX = -11.0f;
    private float maxPosX = 11.45f;

    public int hp = 5;
    public bool gameMode = true;
    public bool gamePause = false;

    public static int point = 0;

    [SerializeField]private TextMeshProUGUI pointTxt;
    [SerializeField]private TextMeshProUGUI lastPoint;

    [SerializeField] private GameObject player;
    public GameObject inGameObject;
    private GameObject setting_Panel;
    private GameObject config_Panel;
    private GameObject GO_Panel;


    [SerializeField]private TextMeshProUGUI hpText;
    // Start is called before the first frame update
    void Start()
    {
        //gameMode = false;
        inGameObject = GameObject.Find("InGame Object");
        setting_Panel = GameObject.Find("Setting Panel");
        GO_Panel = GameObject.Find("Game Over Panel");
        config_Panel = GameObject.Find("Config Panel");
        config_Panel.SetActive(false);
        GO_Panel.SetActive(false);
        setting_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMode) //start to play
        {
            inGameObject.SetActive(true);
            Spawn();
            hpText.text = $"HP = {hp}";
            pointTxt.text = $"Point = {point}";
        }   
        else if(!gameMode && gamePause) // pause game
        {
            inGameObject.SetActive(false);
            player.SetActive(false);

        }
        else if(!gameMode && !gamePause) //gameover
        {
            DestroyAllChild(inGameObject);
        }

        if (hp == 0)
        {
            gameMode = false;
            GO_Panel.SetActive(true);
            lastPoint.text = point.ToString();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseAction();
        }
    }

    int RandomAnimal()
    {
        return Random.Range(0, animals.Count - 1);
    
    }

    Vector3 RandomPos()
    {
        return new Vector3(Random.Range(minPosX, maxPosX), 0, 30);
    }

    void Spawn()
    {
        if (!wait)
            StartCoroutine(MyCoroutine());
        else
            return;
    }

    IEnumerator MyCoroutine()
    {
        wait = true;
        Debug.Log("Start wait time");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("wait done");
        Instantiate(animals[RandomAnimal()], RandomPos(), new Quaternion(0, 180, 0, 0), inGameObject.transform);
        wait = false;
    }

    void DestroyAllChild(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void pauseAction() //setting button
    {
        gameMode = false;
        gamePause = true;
        setting_Panel.SetActive(true);
    }

    public void confirmAction() // confirm button in setting panel
    {
        setting_Panel.SetActive(false);
        gameMode = true;
        gamePause = false;
        player.SetActive(true);
    }

    public void replay() //replay button in gameover panel
    {
        gameMode = true;
        GO_Panel.SetActive(false);
        hp = 5;
        point = 0;
    }

    public void exit()
    {
        Application.Quit();
    }
}

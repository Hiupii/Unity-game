using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> animals;
    [SerializeField] private List<GameObject> characters;
    [SerializeField]private List<Animator> startAnim;



    private bool wait;
    private float minPosX = -11.0f;
    private float maxPosX = 11.45f;

    public int hp = 5;
    public bool gameMode;
    public bool gamePause = false;
    public bool set = true;

    public static int point = 0;

    [SerializeField]private TextMeshProUGUI pointTxt;
    [SerializeField]private TextMeshProUGUI lastPoint;
    [SerializeField] private TextMeshProUGUI chooseText;

    [SerializeField] private GameObject player;
    public GameObject modelPlayer;

    public GameObject inGameObject;
    private GameObject setting_Panel;
    private GameObject config_Panel;
    private GameObject GO_Panel;

    [SerializeField]private GameObject HPBar;
    [SerializeField] private GameObject Point;

    [SerializeField]private GameObject startCam;


    // Start is called before the first frame update
    void Start()
    {
        gameMode = false;
        inGameObject = GameObject.Find("InGame Object");
        setting_Panel = GameObject.Find("Setting Panel");
        GO_Panel = GameObject.Find("Game Over Panel");
        config_Panel = GameObject.Find("Config Panel");
        config_Panel.SetActive(true);
        GO_Panel.SetActive(false);
        setting_Panel.SetActive(false);
        HPBar.SetActive(false);
        Point.SetActive(false);


        for (int i = 0;i<3;i++)
        {
            startAnim[i] = characters[i].GetComponent<Animator>();
        }

        characters[0].SetActive(true);
        characters[1].SetActive(false);
        characters[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(modelPlayer.name == null)
        {
            chooseText.text = "Choosing: ";
        }
        else
        {
            chooseText.text = "Choosing: " + modelPlayer.name;
        }

        if (!set)
        {
            HPBar.SetActive(true);
            Point.SetActive(true);
            if(gameMode) //start to play
            {
                inGameObject.SetActive(true);
                Spawn();
                pointTxt.text = $"{point}";
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

        //chooseChar();
    }

    public void setP1()
    {
        modelPlayer = characters[0];
        characters[0].SetActive(true);
        characters[1].SetActive(false);
        characters[2].SetActive(false);
        startAnim[0].SetInteger("Animation_int", 4);
    }

    public void setP2()
    {
        modelPlayer = characters[1];
        characters[0].SetActive(false);
        characters[1].SetActive(true);
        characters[2].SetActive(false);
        startAnim[1].SetInteger("Animation_int", 6);
    }
    public void setP3()
    {
        modelPlayer = characters[2];
        characters[0].SetActive(false);
        characters[1].SetActive(false);
        characters[2].SetActive(true);
        startAnim[2].SetInteger("Animation_int", 7);
    }

    public void start()
    {
        Instantiate(modelPlayer, player.transform);
        set = !set;
        gameMode = true;
        config_Panel.SetActive(false);
        startCam.SetActive(false);
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
        yield return new WaitForSeconds(2.0f);
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

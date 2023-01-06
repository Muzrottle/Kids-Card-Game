using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public DeckController deckController;
    //public CardController cardController;
    public AudioController audioController;
    public DBManager dbManager;

    bool isStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        deckController = GameObject.Find("Deck").GetComponent<DeckController>();
        dbManager = GameObject.Find("DBManager").GetComponent<DBManager>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        //cardController = GameObject.Find("Deck").GetComponentInChildren<CardController>();

        StartCoroutine(StartGameWait());
        DisplayCardCount(deckController.transform.childCount);
    }

    GameObject card1;
    GameObject card2;
    public string card1_name;
    public string card2_name;
    public int clickCount = 0;
    float timePast = 0;

    public int mistakes = 0;
    public int score = 3000;
   
    public GameObject gameInfo;
    public Text timeText;
    public Text cardText;

    bool isCompleted = false;

    public GameObject completeInfo;
    public Text finalTimeText;
    public Text mistakesText;
    public Text finalScoreText;

    public GameObject scoreInfo;
    public Text scoresText;

    // Update is called once per frame
    void Update()
    {
        card1 = GameObject.Find(card1_name);
        card2 = GameObject.Find(card2_name);

        if (clickCount == 2)
        {
            deckController.isClickable = false;
            
            if (card1.tag == card2.tag)
            {
                audioController.playAnimalAudio(card1.tag);
                StartCoroutine(cardDestroy());
            }
            else
            {
                audioController.playMistakeAudio();
                StartCoroutine(cardOff());
                mistakes = mistakes + 1;
            }

            clickCount = 0;
        }

        if (isStarted && deckController.transform.childCount != 0)
        {
            timePast += Time.deltaTime;
            DisplayTime(timePast);
        }
        else if (isStarted && deckController.transform.childCount == 0)
        {
            isCompleted = true;
            isStarted = false;
        }

        if (isCompleted)
        {
            audioController.playFinishAudio();
            //firebaseManager.SaveData();
            DisplayCompleteInfo();
            isCompleted = false;
        }

    }

    IEnumerator StartGameWait()
    {
        yield return new WaitForSeconds(3f);
        StartGame();
    }

    void StartGame()
    {
        for (int i = 0; i < deckController.transform.childCount; i++)
            deckController.transform.GetChild(i).GetComponentInChildren<CardController>().isClicked = false;

        //for (int i = 0; i < deckController.transform.childCount; i++)
        //{
        //    deckController.transform.GetChild(i).GetComponentInChildren<CardController>().isClicked = false;
        //    deckController.transform.GetChild(i).GetComponentInChildren<CardController>().flipAudioEnabled = true;
        //}

        deckController.isClickable = true;
        isStarted = true;
    }

    IEnumerator cardDestroy()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(card1);
        Destroy(card2);
        
        deckController.isClickable = true;
        
        yield return new WaitForSeconds(0f);
        DisplayCardCount(deckController.transform.childCount);
    }

    IEnumerator cardOff()
    {
        yield return new WaitForSeconds(1.8f);
        card1.GetComponent<CardController>().isClicked = false;
        card2.GetComponent<CardController>().isClicked = false;
        deckController.isClickable = true;
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisplayCardCount(int cardCount)
    {
        cardText.text = "Kalan Kart: " + cardCount;
    }

    public void DisplayCompleteInfo()
    {
        Debug.Log("Girdi.");
        score = score - ((mistakes * 10) + (Mathf.FloorToInt(timePast) * 5));
        finalTimeText.text = "Tamamlanan süre: " + timeText.text;
        mistakesText.text = "Hata sayýsý: " + mistakes;
        finalScoreText.text = "Skor: " + score;
        dbManager.SaveScore();
        gameInfo.SetActive(false);
        completeInfo.SetActive(true);
    }

    public void DisplayScores()
    {
        StartCoroutine(dbManager.LoadScoreboardData());
        completeInfo.SetActive(false);
        scoreInfo.SetActive(true);
    }
}

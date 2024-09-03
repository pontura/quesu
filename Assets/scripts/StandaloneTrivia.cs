using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandaloneTrivia : MainScreen
{
    public TriviaPairButtons pairButton;
    int totalPairs = 1;
    public int serieID = 0;
    int separationY = 176;
    int separation = 370;
    public StandaloneTimeManager timerManager_p1;
    public StandaloneTimeManager timerManager_p2;
    int itemId;
    public FeedbackManager feedbackManager;

    public ResultsPlayer resultsPlayer1;
    public ResultsPlayer resultsPlayer2;

    MultiplayerData multiplayerData;

    public Text score1;
    public Text score2;

    StandaloneInputManager.types player1_result;
    StandaloneInputManager.types player2_result;

    public  bool player1_timeOver;
    public bool player2_timeOver;

    public bool player1_done;
    public bool player2_done;
    public bool isDone;

    public GameObject ready1;
    public GameObject ready2;

    StandaloneFeedbackButton standaloneFeedbackButton1;
    StandaloneFeedbackButton standaloneFeedbackButton2;
    int timeover;
    int rondaID;
    private void Start()
    {
        timeover = Data.Instance.settings.timeover;
         multiplayerData = UserData.Instance.GetComponent<MultiplayerData>();
    }
    public float timer;
    public bool isTimerOn;
    bool loadingQuestion;
    private void Update()
    {
        if(isTimerOn)
        {
            timer += Time.deltaTime;
            if (timer > timeover)
                RondaTimeOver();
        }
    }
    public override void OnEnabled()
    {
        pairButton.gameObject.SetActive(false);
        score1.text = "0";
        score2.text = "0";
    }
    public override void OnReset()
    {
        ready1.SetActive(false);
        ready2.SetActive(false);
        player1_done = false;
        player2_done = false;
        isDone = false;
        resultsPlayer1.Hide();
        resultsPlayer2.Hide();
        pairButton.gameObject.SetActive(false);
    }

    public override void OnInit()
    {
        player1_timeOver = false;
        player2_timeOver = false;

        resultsPlayer1.Hide();
        resultsPlayer2.Hide();

        Events.OnResetTrivia();
        itemId = 0;
        timerManager_p1.Init(this, Data.Instance.settings.triviaDuration);
        timerManager_p2.Init(this, Data.Instance.settings.triviaDuration);
        LoopUntilReady();
        feedbackManager.Init();       
    }
    
    void LoopUntilReady()
    {
        if (Data.Instance.triviaData.loaded)
        {
            Init();
            return;
        }
        Invoke("LoopUntilReady", 0.1f);
    }
    public void Init()
    {
        OnReset();
        LoadNewSerie();
        serieID++;
    }
    public void LoadNewSerie()
    {        
        if (player1_timeOver && player2_timeOver)
            return;
        StartCoroutine(NewSerie());
    }
    IEnumerator NewSerie()
    {
        loadingQuestion = true;
        Events.OnSoundFX("swipe");

        isTimerOn = true;
        LoadPair();

        yield return new WaitForSeconds(2);
        loadingQuestion = false;
        if (!player1_timeOver)
            timerManager_p1.SetState(true);
        if (!player2_timeOver)
            timerManager_p2.SetState(true);

        Events.OnMusic("clock");
    }
    bool CanInteract()
    {
        if (isDone || loadingQuestion)
            return false;
        return true;
    }

    Vector2 diffYears;
    public void LoadPair()
    {   
        diffYears = GetDiffYears();
        itemId++;
        rondaID++;
        ItemData data1 = GetNext();
        ItemData data2 = GetPairFor(data1);
        //si ya no encuentra items resetea todo:
        if (data2==null)
        {
            itemId = 0;
            Data.Instance.triviaData.RefreshAll();
            LoadPair();
            return;
        }
        pairButton.gameObject.SetActive(true);

        standaloneFeedbackButton1 = pairButton.button1.GetComponent<StandaloneFeedbackButton>();
        standaloneFeedbackButton2 = pairButton.button2.GetComponent<StandaloneFeedbackButton>();

        standaloneFeedbackButton1.Reset();
        standaloneFeedbackButton2.Reset();

        pairButton.InitStandalone(this, data1, data2);
        Events.OnSoundFX("boing");
        Debug.Log("rondaID " + rondaID + "   itemId : " + itemId + "   diffYears: " + diffYears + "     data1.year " + data1.year + "    data2.year " + data2.year);
    }
    //para pelis:
    Vector2 GetDiffYears()
    {
        if (rondaID == 0)
            return new Vector2(1000, 6);
        else if (rondaID == 1)
            return new Vector2(15, 6);
        else if (rondaID == 2)
            return new Vector2(12, 4);
        else if (rondaID < 5)
            return new Vector2(8, 3);
        else
            return new Vector2(5, 2);
    }
    Vector2 GetDiffYears___________________________________old()
    {
        if (rondaID == 0)
            return new Vector2(1000, 30);
        else if (rondaID == 1)
            return new Vector2(30, 20);
        else if (rondaID == 2)
            return new Vector2(22, 10);
        else if (rondaID < 5)
            return new Vector2(15, 5);
        else if (rondaID < 9)
            return new Vector2(10, 3);
        else if (rondaID < 12)
            return new Vector2(6, 1);
        else
            return new Vector2(5, 1);
    }
    ItemData GetNext()
    {
        //	print ("pairID " + pairID + "  itemId " + itemId + " length: "+ Data.Instance.triviaData.triviaContent.all.Length);
        if (Data.Instance.triviaData.triviaContent.all.Count <= itemId)
            itemId = 0;
        ItemData data = Data.Instance.triviaData.triviaContent.all[itemId];
        data.usedInGame = true;
        itemId++;
        return data;
    }
    ItemData GetPairFor(ItemData firstPair)
    {
        int year = firstPair.year;
        int id = 0;
        foreach (ItemData itemData in Data.Instance.triviaData.triviaContent.all)
        {
            id++;
            int year1 = firstPair.year;
            int year2 = itemData.year;
            int difYearsOfThisPair = Mathf.Abs(year1 - year2);
            if (
                !itemData.usedInGame 
                && difYearsOfThisPair < diffYears[0] 
                && difYearsOfThisPair > diffYears[1] 
                && itemId < id
                )
            {
                print("difYearsOfThisPair : " + difYearsOfThisPair);
                itemData.usedInGame = true;
                itemId = id;
                return itemData;
            }
        }
        return null;
    }
    public void RondaTimeOver()
    {
        OnStandaloneKeyDown(StandaloneInputManager.types.NONE);
        CheckResult();
    }
    public void TimeOver(int standalonePlayerID)
    {

        if (standalonePlayerID == 1)
            player1_timeOver = true;
        if (standalonePlayerID == 2)
            player2_timeOver = true;

        if (player1_timeOver && player2_timeOver)
        {
            StopAllCoroutines();
            CancelInvoke();
            LoadScreen(2, true);
            Events.OnMusic("");
            Events.OnSoundFX("bell");
        } else
            CheckResult();
    }
   
    public override void OnStandaloneKeyDown(StandaloneInputManager.types type)
    {
        if (!CanInteract())
            return;


        if (!player1_timeOver && 
                (type == StandaloneInputManager.types.NONE  || 
                    (!player1_done && type == StandaloneInputManager.types.P1_LEFT 
                    ||  type == StandaloneInputManager.types.P1_RIGHT)
                    )
            )
        {
            ready1.SetActive(true);
            timerManager_p1.SetState(false);
            timerManager_p1.ShowReady(true);
            player1_result = type;
            player1_done = true;
        }

        if (!player2_timeOver &&
               (type == StandaloneInputManager.types.NONE ||
                   (!player2_done && type == StandaloneInputManager.types.P2_LEFT
                   || type == StandaloneInputManager.types.P2_RIGHT)
                   )
           )
        {
            ready2.SetActive(true);
            timerManager_p2.SetState(false);
            timerManager_p2.ShowReady(true);
            player2_result = type;
            player2_done = true;
        }
        CheckResult();
    }
    void CheckResult()
    {
        if (isDone)
            return;

        

        if ((player1_done || player1_timeOver) && (player2_done || player2_timeOver))
        {

            isTimerOn = false;
            timer = 0;

            standaloneFeedbackButton1.Init(player1_result, player2_result);
            standaloneFeedbackButton2.Init(player1_result, player2_result);

            isDone = true;
            pairButton.SetBothButtonsReady();

            float timer1 = timerManager_p1.GetResult();
            int score1;
            if (player1_timeOver)
                score1 = 0;
            else if (pairButton.button1.win && player1_result == StandaloneInputManager.types.P1_LEFT || pairButton.button2.win && player1_result == StandaloneInputManager.types.P1_RIGHT)
                score1 = (int)(timer1 * 100);
            else
                score1 = 0;

            float timer2 = timerManager_p2.GetResult();
            int score2;
            if (player2_timeOver)
                score2 = 0;
            else if (pairButton.button1.win && player2_result == StandaloneInputManager.types.P2_LEFT || pairButton.button2.win && player2_result == StandaloneInputManager.types.P2_RIGHT)
                score2 = (int)(timer2 * 100);
            else
                score2 = 0;

            int win1 = 0;
            int win2 = 0;

            if (!player1_timeOver)
            {
                if (score1 > 0)
                    timerManager_p1.AddTime(Data.Instance.settings.timeWin);
                else
                    timerManager_p1.AddTime(Data.Instance.settings.timeLose);
            }
            if (!player2_timeOver)
            {
                if (score2 > 0)
                    timerManager_p2.AddTime(Data.Instance.settings.timeWin);
                else
                    timerManager_p2.AddTime(Data.Instance.settings.timeLose);
            }

            if(score1>0 ||  score2>0)
                Events.OnSoundFX("correct");

            if (score1 == score2)
            {
                if (score1 > 0 && score2 > 0)
                {
                    win1 = 1;
                    win2 = 1;
                }
                else
                {
                    //pierden los 2:
                    Events.OnSoundFX("wrong");
                }
            } else if (score1 > 0 && score1 > score2)
            {
                win1 = 2;

                if(score2 > 0)
                    win2 = 1;
            }
            else if (score2 > 0 && score2 > score1)
            {
                if (score1 > 0)
                    win1 = 1;

                win2 = 2;
            }
            if (!player1_timeOver)
            {
                multiplayerData.score_player_1 += score1;
                resultsPlayer1.Init(win1, score1, timer1);               
            }
            if (!player2_timeOver)
            {
                multiplayerData.score_player_2 += score2;
                resultsPlayer2.Init(win2, score2, timer2);
            }
            SetScores();
            StartCoroutine(NextDelayeds());

            player1_result = StandaloneInputManager.types.NONE;
            player2_result = StandaloneInputManager.types.NONE;
        }
    }
    IEnumerator NextDelayeds()
    {
        Events.OnMusic("");
        yield return new WaitForSeconds(1);
        ready1.SetActive(false);
        ready2.SetActive(false);
        yield return new WaitForSeconds(2.1f);
        Init();
    }
    void SetScores()
    {
        score1.text = Utils.SetFormatedNumber(multiplayerData.score_player_1.ToString());
        score2.text = Utils.SetFormatedNumber(multiplayerData.score_player_2.ToString());
    }
}

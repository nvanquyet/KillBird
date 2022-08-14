using System.Collections;
using UnityEngine;

public class ControlGame : Singleton<ControlGame>
{
    public Bird[] birdPrefabs;
    public float spawnTime;

    private float m_numberOfBirds = 0f;
    public int point = 0;
    private int timeLimited = 10;
    private bool endGame = false;

    IEnumerator Begin()
    {
        while (!endGame)
        {
            SpawnBird();
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    IEnumerator timeLimit()
    {
        while(!endGame)
        {
            yield return new WaitForSeconds(1);
            GUIManager.Ins.UpdateTime(intToTime(timeLimited));
            timeLimited--;
        }
    }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        GUIManager.Ins.showGameGUI(false);
        GUIManager.Ins.UpdateScore(point);
    }

    public void PlayGame()
    {
        StartCoroutine(Begin());
        StartCoroutine(timeLimit());
        GUIManager.Ins.showGameGUI(true);
    }

    private void Update()
    {
        checkGameEnds();
    }

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;

        float randPos = Random.Range(0f, 1f);

        if(randPos > 0.5f)
        {
            spawnPos = new Vector3(Random.Range(10f, 12f), Random.Range(1f, 3f), 0);
        } else
        {
            spawnPos = new Vector3(-Random.Range(10f, 12f), Random.Range(1f, 3f), 0);
        }

        if(birdPrefabs != null && birdPrefabs.Length > 0)
        {
            int randInd = Random.Range(0,birdPrefabs.Length);
            if (birdPrefabs[randInd] != null)
            {
                Bird birdClone = Instantiate(birdPrefabs[randInd],spawnPos,Quaternion.identity);
                m_numberOfBirds++;
            }
        }
    }


    private void checkGameEnds()
    {
        if(timeLimited <= 0)
        {
            string title = null;
            string content = null;
            float conditionToWin = 0.8f * m_numberOfBirds;
            endGame = true;
            if (point >= conditionToWin)
            {
                PlayAudio("win");
                title = "You win!";
                if(point >= Prefabs.get_bestScores())
                { 
                    content = "New Best Score: " + point;
                    Prefabs.set_bestScores(point);
                }
                else
                {
                    content =  "Best score: " + Prefabs.get_bestScores();
                }
            }
            if(point < conditionToWin)
            {
                title = "You lose!";
                PlayAudio("lose");
                if (point >= Prefabs.get_bestScores())
                {
                    content = "New Best Score: " + point;
                    Prefabs.set_bestScores(point);
                }
                else
                {
                    content = "Best score: " + Prefabs.get_bestScores();
                }
            }
            GUIManager.Ins.EndPanel.UpdateDialog(title, content);
            GUIManager.Ins.EndPanel.show(true);
            GUIManager.Ins.CurDialog = GUIManager.Ins.EndPanel;
        }
        else
        {
            endGame = false;
        }
    }

    private void PlayAudio(string audioName)
    {
        if(audioName.Equals("win")){
            ControlAud.Ins.PlaySound(ControlAud.Ins.win);
        }
        if (audioName.Equals("lose"))
        {
            ControlAud.Ins.PlaySound(ControlAud.Ins.lose);
        }
    }

    private string intToTime(int toTime)
    {
        float minutes = Mathf.Floor(toTime / 60);
        float seconds = Mathf.RoundToInt(toTime % 60);
        return minutes.ToString("00") + " : " + seconds.ToString("00");
    }

}

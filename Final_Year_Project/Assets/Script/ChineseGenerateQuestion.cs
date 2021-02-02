﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.CorgiEngine;

public class ChineseGenerateQuestion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button[] ansBtn;
    [SerializeField] Text questionTitle;
    [SerializeField] GameObject questionView;
    [SerializeField] GameObject correctFalse;
    [SerializeField] Text scoretext;
    [SerializeField] Text correctorfalsetext;
    [SerializeField] Button backtogame;
    [SerializeField] GameObject myCharacter;
    private Button correctAnsBtn;
    private List<ChineseQuestion> questionsList = new List<ChineseQuestion>();
    private int skippedIndex;
    public GameObject a;

    public void setObject(GameObject a) {
        this.a = a;
    }

    

    public void createQuestion()
    {
        string json = File.ReadAllText(Application.dataPath + "/ChineseQuestion.json");
        ChineseQuestion[] questions = ChineseJsonHelper.FromJson<ChineseQuestion>(json);
        foreach (ChineseQuestion q in questions) {
            questionsList.Add(q);
        }
        correctAnsBtn = ansBtn[RandomButtonIndex()];
        Shuffle(questionsList);
        questionTitle.text = questionsList[0].question;
        for (int i = 0; i < ansBtn.Length; i++)
        {
            Button btn = ansBtn[i];
            Text text = btn.GetComponentInChildren<Text>();
            if (GameObject.ReferenceEquals(btn, correctAnsBtn))
            {
                skippedIndex = i;
                continue;
            }
            if (i != ansBtn.Length - 1)
            {
                text.text = questionsList[0].wrongAns[i];
            }
            else
            {
                text.text = questionsList[0].wrongAns[skippedIndex];
            }

        }
        correctAnsBtn.GetComponentInChildren<Text>().text = questionsList[0].correctAns;
    }

    int RandomButtonIndex()
    {
        int index = Random.Range(0, 4);
        return index;
    }

    void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    void BackToGame() {
        a.SetActive(false);
        questionView.SetActive(false);
        myCharacter.SetActive(true);
    }

    void CheckAnswer(Button btn) 
    {
        if (btn == correctAnsBtn)
        {
            correctFalse.SetActive(true);
            correctorfalsetext.text = "You Are Correct!!!!";
            scoretext.text = "Score +10";
            CorgiEnginePointsEvent.Trigger(PointsMethods.Add, 10);

        }
        else 
        {
            correctFalse.SetActive(true);
            correctorfalsetext.text = "Oh No It Is False!";
            scoretext.text = "Score -10";
            CorgiEnginePointsEvent.Trigger(PointsMethods.Add, -10);
        }
        
    }

    public void ChangeActive() {
        correctFalse.SetActive(false);
    }

    void Start()
    {

        ansBtn[0].onClick.AddListener(() => CheckAnswer(ansBtn[0]));
        ansBtn[1].onClick.AddListener(() => CheckAnswer(ansBtn[1]));
        ansBtn[2].onClick.AddListener(() => CheckAnswer(ansBtn[2]));
        ansBtn[3].onClick.AddListener(() => CheckAnswer(ansBtn[3]));
        backtogame.onClick.AddListener(() => BackToGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

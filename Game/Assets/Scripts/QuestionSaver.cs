using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSaver : MonoBehaviour
{
    public static QuestionSaver Storer;
    public Question currentQuestion;
    public Level[] levels;
    public Level currentLevel;
    public int currentLevelIndex;

    //states
    public bool isTheLastQuestion;
    public bool hasAnsweredCorrectly;

    private void Awake() {
        if (!QuestionSaver.Storer) {
            Storer = this;
            DontDestroyOnLoad(Storer);
            ResetQuiz();
        } else Destroy(this.gameObject);
    }

    public void ResetQuiz() {
        currentLevelIndex = 0;
        currentLevel = levels[0];
        currentLevel.dificultQuestions = new List<Question>(currentLevel.questionsData);
    }
}

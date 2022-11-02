using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.SceneManagement;
public class QuestionSaver : MonoBehaviour
{
    public static QuestionSaver Storer;
    public Question currentQuestion;
    public Level[] levels;
    public Level currentLevel;

    public int currentLevelIndex;
    public int currentQuestionIndex;
    public int totalOfQuestionsUsed;
    public int currentOfTries;

    //states
    public bool isTheLastQuestion;
    public bool hasAnsweredCorrectly;
    public bool currentLevelHasChanged;

    private void Awake() {
        if (!QuestionSaver.Storer) {
            Storer = this;
            DontDestroyOnLoad(Storer);
            ResetQuiz();
            StartRemainingQuestions();
        } else Destroy(this.gameObject);
    }

    private void Update() {
        if(Has.Changed("currentLevelIndex", currentLevelIndex) && currentLevelIndex > 0) {
            currentLevel = levels[currentLevelIndex];
            StartRemainingQuestions();
            currentLevelHasChanged = true;

            if(currentLevelIndex == levels.Length - 1 && currentQuestionIndex >= currentLevel.totalLevelQestions) {
                print("level index: " + currentLevelIndex);
                print("On last Level of Questions");
            }
        }
    }

    public void ResetQuiz() {
        currentQuestionIndex = 0;
        currentOfTries = 3;
        currentLevelIndex = 0;
        currentLevel = levels[0];
        totalOfQuestionsUsed = 0;
    }

    public void StartRemainingQuestions() {
        currentLevel.remainingQuestions = new List<Question>(currentLevel.allQuestions);
        currentQuestionIndex = 0;
    }
}

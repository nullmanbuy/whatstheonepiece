using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utils;
public class ShowQuestionController : MonoBehaviour
{
    private QuestionSaver _storer => QuestionSaver.Storer;
    public TMP_Text questionText;
    public TMP_Text currentLevelText;
    private Level _currentLevel;
    public float timeToShowQuestion;

    private void Start() {
        _storer.currentLevel = _storer.levels[_storer.currentLevelIndex];
        _currentLevel = QuestionSaver.Storer.currentLevel;
        currentLevelText.SetText("Nível - " + _currentLevel.dificultName.ToString());
        QuestionSaver.Storer.currentQuestion = SetRandomQuestion(_currentLevel.dificultQuestions);

        questionText.SetText(_storer.currentQuestion.questionAsk + "?");
        SceneCaller.instance.CallScene("QuestionScene", timeToShowQuestion);
    }
    public Question SetRandomQuestion(List<Question> questions) {
        _currentLevel = QuestionSaver.Storer.currentLevel;
        int randomIndex = Random.Range(0, questions.Count);
        QuestionSaver.Storer.currentQuestion = _currentLevel.dificultQuestions[randomIndex];

        Question currentQuestion = QuestionSaver.Storer.currentQuestion;
        _currentLevel.dificultQuestions.RemoveAt(randomIndex);

        if (_currentLevel.dificultQuestions.Count == 0) _storer.isTheLastQuestion = true;
        return currentQuestion;
    }
}

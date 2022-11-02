using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public static TransitionController _controller;
    [SerializeField] Canvas canvas;
    [SerializeField] Animator transitionAnim;
    public bool enterFirstTime;

    public UnityEvent OnStart;
    public UnityEvent OnOut;

    void Awake() {
        if(!TransitionController._controller) {
            _controller = this;
            DontDestroyOnLoad(_controller);
        } else {
            _controller.OnStart?.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        Coroutines.DoAfter(() => enterFirstTime = true, 2f, this);
        OnStart.AddListener(StartTransition);
        OnOut.AddListener(OutTransition);
    }

    private void OnDestroy() {
        OnStart.RemoveListener(StartTransition);
        OnOut.RemoveListener(OutTransition);
    }

    public void OutTransition() {
        transitionAnim.SetTrigger("out");
    }

    public void StartTransition() {
        StartCoroutine(ResetTimeScale());
        Time.timeScale = 0;
        transitionAnim.SetTrigger("in");     
    }

    public IEnumerator ResetTimeScale() {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
    }
}

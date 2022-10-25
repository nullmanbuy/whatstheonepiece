using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class SceneCaller : MonoBehaviour
{
    public static SceneCaller instance;

    private void Awake() {
        instance = this;
    }
    public void CallScene(string sceneName, float sceneCooldown) {
        Coroutines.DoAfter(() => {
            SceneManager.LoadScene(sceneName);
        }, sceneCooldown, this);
    }

    public void CallSceneByString(string sceneName) {
        Coroutines.DoAfter(() => {
            SceneManager.LoadScene(sceneName);
        }, 1f, this);
    }
}

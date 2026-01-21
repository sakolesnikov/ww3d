using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger {

    private string targetSceneName;
    public TextMeshProUGUI ProgressText { get; set; }

    public void ReloadLevel() {
        LoadLevel("level1");
    }

    public async UniTask LoadSceneFromAction() {
        if (string.IsNullOrEmpty(targetSceneName)) {
            SceneManager.LoadScene("menu");
            return;
        }

        await LoadLevelAsync(targetSceneName);
        targetSceneName = null;
    }

    private async UniTask LoadLevelAsync(string sceneName) {
        var operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone) {
            var progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (ProgressText != null) {
                var message = $"{progress * 100}%";
                ProgressText.text = message;
            }

            if (operation.progress >= 0.9f) {
                await UniTask.Delay(TimeSpan.FromSeconds(0.3f));

                if (ProgressText != null) {
                    var message = "100%";
                    ProgressText.text = message;
                }
            }

            await UniTask.Yield();
        }
    }

    public void LoadLevel(string level) {
        targetSceneName = level;
        SceneManager.LoadScene("loading");
    }

    public void LoadMenu() {
        LoadLevel("menu");
    }

}
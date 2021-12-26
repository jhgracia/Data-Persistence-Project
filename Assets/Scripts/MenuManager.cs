using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    public InputField nameInput;
    public Text[] topScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        DisplayTopScores();

        if (!string.IsNullOrEmpty(DataManager.Instance.m_lastPlayer.name))
        {
            nameInput.text = DataManager.Instance.m_lastPlayer.name;
        }
    }


    void DisplayTopScores()
    {

        for (int i = 0; i < DataManager.Instance.m_topPLayers.Length; i++)
        {
            topScoreDisplay[i].text = $"{DataManager.Instance.m_topPLayers[i].name} : {DataManager.Instance.m_topPLayers[i].score}";
        }
    }

    public void StartGame()
    {
        if (nameInput.text != "")
        {
            Debug.Log($"revisando {DataManager.Instance.m_topPLayers[0].name}, {DataManager.Instance.m_topPLayers[0].score}");
            DataManager.Instance.m_lastPlayer.name = nameInput.text;
            DataManager.Instance.m_lastPlayer.score = 0;
            Debug.Log($"dejando {DataManager.Instance.m_topPLayers[0].name}, {DataManager.Instance.m_topPLayers[0].score}");
            SceneManager.LoadScene(1);
        }
    }

    public void QuitGame()
    {
        DataManager.Instance.SavePlayers();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

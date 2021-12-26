using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public Player m_lastPlayer;
    public Player[] m_topPLayers;

    string saveFileName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFileName = Application.persistentDataPath + "/savefile.json";
        LoadPlayers();
    }

    [System.Serializable]
    public class Player
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    class SaveData
    {
        public Player lastPlayer;
        public Player[] topPlayers;
    }

    public void SavePlayers()
    {
        SaveData data = new SaveData();

        data.lastPlayer = m_lastPlayer;
        data.topPlayers = m_topPLayers;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFileName, json);
    }

    public void LoadPlayers()
    {
        if (File.Exists(saveFileName))
        {
            string json = File.ReadAllText(saveFileName);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_lastPlayer = data.lastPlayer;
            m_topPLayers = data.topPlayers;
        }
        else
        {
            m_lastPlayer = new Player();
            m_topPLayers = new Player[5];
        }

        for(int i = 0; i < m_topPLayers.Length; i++)
        {
            if(m_topPLayers[i] == null)
            {
                m_topPLayers[i] = new Player();
            }

            if (string.IsNullOrEmpty(m_topPLayers[i].name))
            {
                m_topPLayers[i].name = "Player " + (i + 1);
                m_topPLayers[i].score = 0;
            }
        }
    }

    public void OrganizeTopPlayers()
    {
        Player playerToCompare = new Player();
        Player tempPlayer = new Player();

        playerToCompare.name = m_lastPlayer.name;
        playerToCompare.score = m_lastPlayer.score;

        for (int i = 0; i < m_topPLayers.Length; i++)
        {
            if (playerToCompare.score >= m_topPLayers[i].score)
            {
                tempPlayer.name = m_topPLayers[i].name;
                tempPlayer.score = m_topPLayers[i].score;

                m_topPLayers[i].name = playerToCompare.name;
                m_topPLayers[i].score = playerToCompare.score;

                playerToCompare.name = tempPlayer.name;
                playerToCompare.score = tempPlayer.score;
            }
        }
    }
}

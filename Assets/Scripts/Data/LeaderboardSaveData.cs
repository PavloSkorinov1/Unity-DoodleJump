using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System; 
using Newtonsoft.Json;

namespace Data
{
    [Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public int score;
        public float distance;
        
        public LeaderboardEntry(string name, int scoreVal, float distanceVal, string dateVal)
        {
            playerName = name;
            score = scoreVal;
            distance = distanceVal;
        }
    }
    
    [Serializable]
    public class LeaderboardData
    {
        public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
    }
    
    public class LeaderboardSaveData : MonoBehaviour
{
    public static LeaderboardSaveData Instance { get; private set; }
    
    private const string LEADERBOARD_KEY = "KingdomJumpLeaderboard";
    public int maxLeaderboardEntries = 10;

    private LeaderboardData _leaderboardData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        LoadLeaderboard();
    }
    
    public void SaveLeaderboardEntry(string playerName, int score, float distance)
    {
        LeaderboardEntry newEntry = new LeaderboardEntry(
            playerName,
            score,
            distance,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm")
        );

        _leaderboardData.entries.Add(newEntry);
        
        _leaderboardData.entries = _leaderboardData.entries
                                        .OrderByDescending(e => e.score)
                                        .ThenByDescending(e => e.distance)
                                        .ToList();
        
        if (_leaderboardData.entries.Count > maxLeaderboardEntries)
        {
            _leaderboardData.entries.RemoveRange(maxLeaderboardEntries, _leaderboardData.entries.Count - maxLeaderboardEntries);
        }
        
        string json = JsonConvert.SerializeObject(_leaderboardData);
        
        PlayerPrefs.SetString(LEADERBOARD_KEY, json);
        PlayerPrefs.Save();
        
    }
    
    private void LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey(LEADERBOARD_KEY))
        {
            string json = PlayerPrefs.GetString(LEADERBOARD_KEY);
            try
            {
                _leaderboardData = JsonConvert.DeserializeObject<LeaderboardData>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error deserializing data: {e.Message}");
                _leaderboardData = new LeaderboardData();
            }
        }
        else
        {
            _leaderboardData = new LeaderboardData();
        }
        if (_leaderboardData.entries == null) {
            _leaderboardData.entries = new List<LeaderboardEntry>();
        }
    }
    
    public List<LeaderboardEntry> GetLeaderboardEntries()
    {
        return new List<LeaderboardEntry>(_leaderboardData.entries);
    }
    
    [ContextMenu("Clear Leaderboard Data")]
    public void ClearLeaderboardData()
    {
        PlayerPrefs.DeleteKey(LEADERBOARD_KEY);
        PlayerPrefs.Save();
        _leaderboardData = new LeaderboardData();
        Debug.Log("Data cleared from PlayerPrefs.");
    }
}

}

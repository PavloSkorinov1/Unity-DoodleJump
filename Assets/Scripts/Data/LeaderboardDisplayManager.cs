using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

namespace Data
{
    public class LeaderboardDisplayManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform leaderboardContentParent;
        [SerializeField] private GameObject leaderboardEntryPrefab;

        private LeaderboardSaveData _leaderboardSaveData;
        
        void Awake()
    {
        _leaderboardSaveData = LeaderboardSaveData.Instance;
        if (_leaderboardSaveData == null)
        {
            Debug.LogError("LeaderboardDisplayManager: LeaderboardSaveData not found");
            enabled = false;
        }
    }

    public void RefreshLeaderboardDisplay()
    {
        if (_leaderboardSaveData == null)
        {
            Debug.LogError("LeaderboardDisplayManager: LeaderboardSaveData is null.");
            return;
        }
        
        ClearLeaderboardEntries();
        
        List<LeaderboardEntry> entries = _leaderboardSaveData.GetLeaderboardEntries();

        if (entries.Count == 0)
        {
            SpawnNoEntriesMessage();
            return;
        }
        
        for (int i = 0; i < entries.Count; i++)
        {
            LeaderboardEntry entryData = entries[i];
            
            GameObject entryGameObject = Instantiate(leaderboardEntryPrefab, leaderboardContentParent);
            entryGameObject.name = $"LeaderboardEntry_{i + 1}_{entryData.playerName}";
            
            TextMeshProUGUI nameText = entryGameObject.transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = entryGameObject.transform.Find("Score")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI distanceText = entryGameObject.transform.Find("Distance")?.GetComponent<TextMeshProUGUI>();
            Image playerIcon = entryGameObject.transform.Find("PlayerIcon")?.GetComponent<Image>();
            
            if (nameText != null) nameText.text = entryData.playerName;
            if (scoreText != null) scoreText.text = $"Coins: {entryData.score.ToString(CultureInfo.InvariantCulture)}";
            if (distanceText != null) distanceText.text = $"Dist: {Mathf.FloorToInt(entryData.distance).ToString(CultureInfo.InvariantCulture)}m";
            
        }
    }
    
    private void ClearLeaderboardEntries()
    {
        if (leaderboardContentParent == null)
        {
            Debug.LogError("LeaderboardDisplayManager: leaderboardContentParent is not assigned");
            return;
        }

        foreach (Transform child in leaderboardContentParent)
        {
            Destroy(child.gameObject);
        }
    }
    
    private void SpawnNoEntriesMessage()
    {
        if (leaderboardContentParent != null)
        {
            GameObject messageObj = new GameObject("NoEntriesMessage");
            messageObj.transform.SetParent(leaderboardContentParent);
            TextMeshProUGUI messageText = messageObj.AddComponent<TextMeshProUGUI>();
            messageText.text = "Leaderboard is empty! Play game and submit your record!";
            messageText.fontSize = 64;
            messageText.alignment = TextAlignmentOptions.Center;
            messageText.color = Color.white; 
            
            RectTransform rectT = messageText.rectTransform;
            rectT.anchorMin = new Vector2(0.5f, 0.5f); 
            rectT.anchorMax = new Vector2(0.5f, 0.5f); 
            rectT.pivot = new Vector2(0.5f, 0.5f);
            rectT.anchoredPosition = Vector2.zero;
            rectT.sizeDelta = new Vector2(leaderboardContentParent.GetComponent<RectTransform>().rect.width * 0.8f, 500);
        }
    }
    }
}

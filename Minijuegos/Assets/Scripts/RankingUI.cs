using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Firebase.Firestore;

public class RankingUI : MonoBehaviour
{
    [Header("UI")]
    public Transform content;          
    public GameObject rowPrefab;       
    public TMP_Text statusText;        

    [Header("Ranking Config")]
    public string collectionName = "scores_game1"; 
    public int topN = 20;

    FirebaseFirestore db;

    void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    public async void RefreshRanking()
    {
        await LoadTopScores();
    }

    async Task LoadTopScores()
    {
        try
        {
            if (statusText) statusText.text = "Cargando ranking...";
            ClearContent();

            
            Query q = db.Collection(collectionName).OrderByDescending("score").Limit(topN);

            QuerySnapshot snap = await q.GetSnapshotAsync();

            foreach (DocumentSnapshot doc in snap.Documents)
            {
                string playerName = "Anónimo";
                int score = 0;
                string dateStr = "-";

                if (doc.ContainsField("playerName"))
                {
                    string n = doc.GetValue<string>("playerName");
                    if (!string.IsNullOrWhiteSpace(n)) playerName = n;
                }

                if (doc.ContainsField("score"))
                    score = doc.GetValue<int>("score");

                if (doc.ContainsField("date"))
                {
                    Timestamp ts = doc.GetValue<Timestamp>("date");
                    DateTime dt = ts.ToDateTime().ToLocalTime();
                    dateStr = dt.ToString("dd/MM/yyyy HH:mm");
                }

                
                GameObject row = Instantiate(rowPrefab, content);
                var texts = row.GetComponentsInChildren<TMP_Text>();

                
                texts[0].text = playerName;
                texts[1].text = score.ToString();
                texts[2].text = dateStr;
            }

            if (statusText) statusText.text = $"Top {snap.Count} cargado.";
        }
        catch (Exception e)
        {
            Debug.LogError("Error cargando ranking: " + e);
            if (statusText) statusText.text = "Error cargando ranking (mira consola).";
        }
    }

    void ClearContent()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
            Destroy(content.GetChild(i).gameObject);
    }
}

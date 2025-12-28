using Firebase.Firestore;
using Firebase.Auth;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUploader : MonoBehaviour
{
    public string collectionName = "scores_game1";

    public async void UploadScore(int score, string playerName)
    {
        var db = FirebaseFirestore.DefaultInstance;
        var user = FirebaseAuth.DefaultInstance.CurrentUser;

        string nameToSave = string.IsNullOrWhiteSpace(playerName) ? "Anónimo" : playerName;

        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "uid", user != null ? user.UserId : "no_uid" },
            { "playerName", nameToSave },
            { "score", score },
            { "date", FieldValue.ServerTimestamp }
        };

        await db.Collection(collectionName).AddAsync(data);
    }
}

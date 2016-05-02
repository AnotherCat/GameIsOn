using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    public Text playerListText;

    public RectTransform bgLocalplayerHealthbar;
    public RectTransform fgLocalplayerHealthbar;

    GameManager gm;
    public static UIManager INSTANCE;

    void Awake()
    {
        INSTANCE = this;
    }

    void Start()
    {
        gm = GameManager.INSTANCE;
    }

    void Update()
    {
        playerListText.text = "";
        foreach (KeyValuePair<string, Player> pare in gm.players)
        {
            playerListText.text += pare.Value.transform.name + "[" + pare.Value.currentHp + "]" + "\n";
        }
    }
}

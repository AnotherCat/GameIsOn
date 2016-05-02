using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UIPlayerManager : NetworkBehaviour {

    public static UIPlayerManager INSTANCE;

    public RectTransform bgHealthBar;

    void Awake()
    {
        INSTANCE = this;
    }

    public override void OnStartLocalPlayer()
    {
        bgHealthBar.gameObject.SetActive(false);
        base.OnStartLocalPlayer();
    }
}

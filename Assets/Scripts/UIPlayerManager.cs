using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UIPlayerManager : NetworkBehaviour {

    public RectTransform bgHealthBar;

    public override void OnStartLocalPlayer()
    {
        bgHealthBar.gameObject.SetActive(false);
        base.OnStartLocalPlayer();
    }
}

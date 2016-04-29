using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public int maxHp = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHp;
    public int damage = 25;

    public RectTransform healthbar;

    public int kill;
    public int dead;

    void Start()
    {
        currentHp = maxHp;
        kill = 0;
        dead = 0;
    }

    public void ResetHealth()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }
        currentHp -= amount;
        if(currentHp <= 0)
        {
            currentHp = 0;
            CmdDie();
        }
        
    }

    [Command]
    void CmdDie()
    {
        RpcDie();
    }

    [ClientRpc]
    void RpcDie()
    {
        GetComponent<PlayerNetwork>().Die();
    }

    void OnChangeHealth(int health)
    {
        healthbar.sizeDelta = new Vector2(health * 2, healthbar.sizeDelta.y);
        GameManager.INSTANCE.getPlayer(transform.name).currentHp = health;
    }
}

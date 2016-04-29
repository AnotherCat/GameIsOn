using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerNetwork : NetworkBehaviour {

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
    public Camera fpsCamera;
    public PlayerShooting playerShooting;

    Renderer[] renderers;
    Collider[] colliders;

    public Behaviour[] disableOnDeath;
    private bool[] wasEnabled;
    
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
        Setup();
    }

    public override void OnStartLocalPlayer()
    {
        fpsController.enabled = true;
        fpsCamera.enabled = true;
        playerShooting.enabled = true;
        base.OnStartLocalPlayer();
    }

    public override void OnStartClient()
    {
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.INSTANCE.RegisterPlayer(_netID, _player);
        base.OnStartClient();
    }

    void OnDisable()
    {
        GameManager.INSTANCE.UnregisterPlayer(transform.name);
    }

    void ToggleRenderer(bool toggle)
    {
        for(int i = 0;i < renderers.Length; i++)
        {
            renderers[i].enabled = toggle;
        }
    }
    void ToggleCollider(bool toggle)
    {
        for(int i = 0;i < colliders.Length; i++)
        {
            colliders[i].enabled = toggle;
        }
    }

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for(int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        setDefaults();
    }

    public void setDefaults()
    {
        for(int i = 0;i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        GetComponent<Player>().ResetHealth();
    }

    public void Die()
    {
        ToggleRenderer(false);
        ToggleCollider(false);
        for(int i = 0;i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        
        Transform spawn = NetworkManager.singleton.GetStartPosition();
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;

        yield return new WaitForSeconds(1f);

        setDefaults();
        ToggleRenderer(true);
        ToggleCollider(true);

    }
}

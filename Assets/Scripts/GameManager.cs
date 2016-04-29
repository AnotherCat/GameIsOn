using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE;

	private const string PLAYER_ID_PREFIX = "Player";

    public Dictionary<string, Player> players = new Dictionary<string, Player>();

    void Start()
    {
        INSTANCE = this;
    }

    public void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        _player.transform.name = _playerID;
        players.Add(_playerID, _player);
    }

    public void UnregisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public Player getPlayer(string _playerID)
    {
        return players[_playerID];
    }
}

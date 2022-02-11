using UnityEngine;

public sealed class Spawner
{
    private Transform _playerSpawnPoint;

    public Spawner(Transform playerSpawnPoint)
    {
        _playerSpawnPoint = playerSpawnPoint;
    }

    public PlayerView SpawnPlayer(GameObject prefab)
    {
        GameObject playerGameObject = Object.Instantiate(prefab);
        playerGameObject.transform.position = _playerSpawnPoint.position;
        playerGameObject.transform.rotation = _playerSpawnPoint.rotation;

        if (playerGameObject.TryGetComponent(out PlayerView view))
        {
            return view;
        }
        else
        {
            Debug.Log("PlayerView script is missing...");
            return null;
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = _playerSpawnPoint.position;
        player.transform.rotation = _playerSpawnPoint.rotation;
    }
}
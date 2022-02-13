using UnityEngine;
using UnityEngine.AI;

public sealed class MazeConstructor
{
    private const string TAG = "Generated";
    private const string ROOT = "Maze";
    private MazeDataGenerator _dataGenerator;
    private GameObject _wallPrefab;
    private GameObject _greenZonePrefab;
    private GameObject _deadZonePrefab;
    private GameObject _root;
    private int[,] _data;
    private float _zonePositionY = 0.3f;
    private float _wallPositionY = 1f;
    private float _playerPositionY = 2f;
    private Vector3 _playerSpawnPosition;

    public Vector3 PlayerSpawnPosition => _playerSpawnPosition;

    public void Init(GameObject wallPrefab, GameObject greenZonePrefab, GameObject deadZonePrefab)
    {
        _wallPrefab = wallPrefab;
        _greenZonePrefab = greenZonePrefab;
        _deadZonePrefab = deadZonePrefab;
        _dataGenerator = new MazeDataGenerator();
        _root = new GameObject(ROOT);
    }

    public void GenerateNewMaze(int sizeRows, int sizeColumns, int deadZoneQuantity)
    {
        DisposeOldMaze();

        _data = _dataGenerator.FromDimension(sizeRows, sizeColumns);

        for (var i = 0; i < sizeRows; i++)
        {
            for (var j = 0; j < sizeColumns; j++)
            {
                if (_data[i, j] == 0)
                {
                    continue;
                }

                var wall = Object.Instantiate(_wallPrefab);
                wall.transform.position = new Vector3(i, _wallPositionY, j);
                wall.tag = TAG;
                wall.AddComponent<NavMeshObstacle>();
                wall.transform.parent = _root.transform;
                _data[i, j] = 1;
            }
        }

        _playerSpawnPosition = FindStartPosition(_data.GetUpperBound(0), _data.GetUpperBound(1));

        var greenZone = Object.Instantiate(_greenZonePrefab);
        greenZone.transform.position = FindGoalPosition(_data.GetUpperBound(0), _data.GetUpperBound(1));
        greenZone.tag = TAG;
        greenZone.transform.parent = _root.transform;

        for (var i = 0; i < deadZoneQuantity; i++)
        {
            var deadZone = Object.Instantiate(_deadZonePrefab);
            deadZone.transform.position = FindRandomAvailablePosition();
            deadZone.tag = TAG;
            deadZone.transform.parent = _root.transform;
        }
    }

    public void DisposeOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(TAG);
        foreach (GameObject gameObject in objects)
        {
            Object.Destroy(gameObject);
        }
    }

    private Vector3 FindStartPosition(int rowMax, int columnMax)
    {    
        for (int i = 0; i <= rowMax; i++)
        {
            for (int j = 0; j <= columnMax; j++)
            {
                if (_data[i, j] == 0)
                {
                    _data[i, j] = 1;
                    return new Vector3(i, _playerPositionY, j);
                }
            }
        }

        return Vector3.zero;
    }

    private Vector3 FindGoalPosition(int rowMax , int columnMax)
    {
        for (int i = rowMax; i >= 0; i--)
        {
            for (int j = columnMax; j >= 0; j--)
            {
                if (_data[i, j] == 0)
                {
                    _data[i, j] = 1;
                    return new Vector3(i, _zonePositionY, j);
                }
            }
        }

        return Vector3.zero;
    }

    private Vector3 FindRandomAvailablePosition()
    {
        var result = Vector3.zero;

        int i = Random.Range(0, _data.GetLength(0));
        int j = Random.Range(0, _data.GetLength(1));

        if (_data[i, j] == 0)
        {
            result = new Vector3(i, _zonePositionY, j);
        }
        else
        {
            result = FindRandomAvailablePosition();
        }

        return result;
    }
}
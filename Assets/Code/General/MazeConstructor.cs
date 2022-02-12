using UnityEngine;
using UnityEngine.AI;

public sealed class MazeConstructor
{
    private const string TAG = "Generated";
    private MazeDataGenerator _dataGenerator;
    private GameObject _wallPrefab;
    private GameObject _greenZonePrefab;
    private GameObject _deadZonePrefab;
    private int _startRow;
    private int _startColumn;
    private int _goalRow;
    private int _goalColumn;
    private int[,] _data;
    private float _zonePositionY = 0.5f;
    private float _wallPositionY = 1f;
    private float _playerPositionY = 2f;

    public Vector3 PlayerSpawnPosition
    {
        get 
        {
            _data[_startRow, _startColumn] = 1;
            return new Vector3(_startRow, _playerPositionY, _startColumn); 
        }
    }

    public void Init(GameObject wallPrefab, GameObject greenZonePrefab, GameObject deadZonePrefab)
    {
        _wallPrefab = wallPrefab;
        _greenZonePrefab = greenZonePrefab;
        _deadZonePrefab = deadZonePrefab;
        _dataGenerator = new MazeDataGenerator();
    }

    public void GenerateNewMaze(int sizeRows, int sizeColumns, int deadZoneQuantity)
    {
        DisposeOldMaze();

        _data = _dataGenerator.FromDimension(sizeRows, sizeColumns);

        FindStartPosition();
        FindGoalPosition();

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
            }
        }

        var greenZone = Object.Instantiate(_greenZonePrefab);
        greenZone.transform.position = new Vector3(_goalRow, _zonePositionY, _goalColumn);
        greenZone.tag = TAG;
        _data[_goalRow, _goalColumn] = 1;

        for (var i = 0; i < deadZoneQuantity; i++)
        {
            var deadZone = Object.Instantiate(_deadZonePrefab);
            deadZone.transform.position = FindRandomAvailablePosition();
            deadZone.tag = TAG;
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

    private void FindStartPosition()
    {
        int[,] maze = _data;
        int rowMax = maze.GetUpperBound(0);
        int columnMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rowMax; i++)
        {
            for (int j = 0; j <= columnMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    _startRow = i;
                    _startColumn = j;
                    return;
                }
            }
        }
    }

    private void FindGoalPosition()
    {
        int[,] maze = _data;
        int rowMax = maze.GetUpperBound(0);
        int columnMax = maze.GetUpperBound(1);

        for (int i = rowMax; i >= 0; i--)
        {
            for (int j = columnMax; j >= 0; j--)
            {
                if (maze[i, j] == 0)
                {
                    _goalRow = i;
                    _goalColumn = j;
                    return;
                }
            }
        }
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
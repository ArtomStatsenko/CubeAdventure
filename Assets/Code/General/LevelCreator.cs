using System.Collections.Generic;
using UnityEngine;

public sealed class LevelCreator
{
    private Vector3[,] _levelGrid;
    private int _deadZoneQuantity = 3;
    private GameObject _deadZonePrefab;
    private float _yPosition = 0.2f;
    private int _sideWallQuantity = 2;
    private List<Vector3> _occupiedPoints;
    private GameObject _greenZone;
    private Vector3 _greenZonePosition = new Vector3(8f, 0.2f, 8f);
    private Vector3 _playerPosition = new Vector3(2f, 1f, 2f);

    public LevelCreator(GameObject deadZone, GameObject floor, GameObject greenZone)
    {
        _deadZonePrefab = deadZone;
        _greenZone = greenZone;
        var floorScale = floor.transform.localScale;
        _occupiedPoints = new List<Vector3>();
        _occupiedPoints.Add(_playerPosition);

        CreateLevelGrid(floorScale.x, floorScale.z);
    }

    private void CreateLevelGrid(float floorScaleX, float floorScaleZ)
    {
        int columns = Mathf.RoundToInt(floorScaleX) - _sideWallQuantity;
        int rows = Mathf.RoundToInt(floorScaleZ) - _sideWallQuantity;
        _levelGrid = new Vector3[columns, rows];

        int startX = (-columns / 2) + 4;
        int startZ = (-rows / 2) + 4;
        int endX = _levelGrid.GetLength(0) - 2;
        int endZ = _levelGrid.GetLength(1) - 2;
        int currentX = startX;
        int currentZ = startZ;

        for (var x = 0; x < endX; x++)
        {
            for (var y = 0; y < endZ; y++)
            {
                Vector3 point = new Vector3(currentX, _yPosition, currentZ);
                _levelGrid[x, y] = point;
                currentZ++;
            }
            currentX++;
            currentZ = startZ;
        }
    }

    public void CreateEnvironment()
    {
        CreateGreenZone();
        CreateDeadZones();       
    }

    private void CreateGreenZone()
    {
        GameObject greenZone = Object.Instantiate(_greenZone);
        greenZone.transform.position = _greenZonePosition;
        _occupiedPoints.Add(_greenZonePosition);
    }

    private void CreateDeadZones()
    {
        for (var i = 0; i < _deadZoneQuantity; i++)
        {
            bool isFindingSpace = true;
            Vector3 point = default;

            while (isFindingSpace)
            {
                point = _levelGrid.GetRandomPoint();

                if (!_occupiedPoints.Contains(point))
                {
                    isFindingSpace = false;
                }

                if (_occupiedPoints.Count == _levelGrid.Length)
                {
                    return;
                }
            }

            GameObject deadZone = Object.Instantiate(_deadZonePrefab);
            deadZone.transform.position = point;
            _occupiedPoints.Add(point);
        }
    }

}
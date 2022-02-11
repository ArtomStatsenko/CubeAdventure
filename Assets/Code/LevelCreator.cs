using System.Collections.Generic;
using UnityEngine;

public sealed class LevelCreator
{
    private Vector3[,] _levelGrid;
    private int _deadZoneQuantity = 3;
    private GameObject _deadZonePrefab;
    private float _yPosition = 0.1f;
    private int _sideWallQuantity = 2;
    private List<Vector3> _occupiedPoints;
    private GameObject _greenZone;
    private int _greenZoneX = 8;
    private int _greenZoneZ = 8;
    private GameObject _wallPrefab;

    public LevelCreator(GameObject deadZone, GameObject floor, GameObject greenZone, GameObject wall)
    {
        _deadZonePrefab = deadZone;
        _greenZone = greenZone;
        _wallPrefab = wall;
        var floorScale = floor.transform.localScale;
        _occupiedPoints = new List<Vector3>();

        CreateLevelGrid(floorScale.x, floorScale.z);
    }

    private void CreateLevelGrid(float floorScaleX, float floorScaleZ)
    {
        int columns = Mathf.RoundToInt(floorScaleX) - _sideWallQuantity;
        int rows = Mathf.RoundToInt(floorScaleZ) - _sideWallQuantity;
        _levelGrid = new Vector3[columns, rows];

        int startX = (-columns / 2) + 1;
        int startZ = (-rows / 2) + 1;
        int endX = _levelGrid.GetLength(0) - 1;
        int endZ = _levelGrid.GetLength(1) - 1;
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
        CreateInternalWalls();
        CreateDeadZones();       
    }

    private void CreateInternalWalls()
    {
       
    }

    private void CreateGreenZone()
    {
        Vector3 point = new Vector3(_greenZoneX, _yPosition, _greenZoneZ);
        GameObject greenZone = Object.Instantiate(_greenZone);
        greenZone.transform.position = point;
        _occupiedPoints.Add(point);
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
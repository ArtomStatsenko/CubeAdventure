using Random = UnityEngine.Random;

public sealed class MazeDataGenerator
{
    private float _placementThreshold;

    public MazeDataGenerator()
    {
        _placementThreshold = 0.5f;
    }

    public int[,] FromDimension(int sizeRows, int sizeColumns)
    {
        int[,] maze = new int[sizeRows, sizeColumns];

        int rowMax = maze.GetUpperBound(0);
        int columnMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rowMax; i++)
        {
            for (int j = 0; j <= columnMax; j++)
            {
                if (i == 0 || j == 0 || i == rowMax || j == columnMax)
                {
                    maze[i, j] = 1;
                }

                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > _placementThreshold)
                    {
                        maze[i, j] = 1;

                        int a = Random.value < 0.5f ? 0 : (Random.value < 0.5f ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < 0.5f ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }

        return maze;
    }
}
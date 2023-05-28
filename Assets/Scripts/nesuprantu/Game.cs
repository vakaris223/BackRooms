using TMPro;
using Unity.Jobs;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

using static Unity.Mathematics.math;

public class Game : MonoBehaviour
{
    [SerializeField]
    MazeVisualization visualization;

    [SerializeField]
    int2 mazeSize = int2(10, 10);

    [SerializeField, Tooltip("Use zero for random seed.")]
    int seed;

    bool oneTime = true;

    Maze maze;

    void Awake()
    {
        updateMaze();
    }
    void OnDestroy()
    {
        maze.Dispose();
    }

    private void Update()
    {
        oneTime = true;
        if (Input.GetKeyDown(KeyCode.L) && oneTime)
        {
            maze.Dispose();
            GameObject[] cells = GameObject.FindGameObjectsWithTag("cells");
            foreach (GameObject cell in cells)
                GameObject.Destroy(cell);

            maze = new Maze(mazeSize);
            new GenerateMazeJob
            {
                maze = maze,
                seed = seed != 0 ? seed : Random.Range(1, int.MaxValue)
            }.Schedule().Complete();

            visualization.Visualize(maze);
            oneTime = false;
        }
    }


    public void updateMaze()
    {
       /* maze.Dispose();
        GameObject[] cells = GameObject.FindGameObjectsWithTag("cells");
        foreach (GameObject cell in cells)
            GameObject.Destroy(cell);*/

        maze = new Maze(mazeSize/2);
        new GenerateMazeJob
        {
            maze = maze,
            seed = seed != 0 ? seed : Random.Range(1, int.MaxValue)
        }.Schedule().Complete();

        visualization.Visualize(maze);
    }
    
    

}
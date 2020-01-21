using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// @author Addison Shuppy
/// </summary>
public class SpawnManager1 : MonoBehaviour
{
    /// <summary>
    /// The list of all enemies in the game
    /// </summary>
    [SerializeField] private GameObject[] enemyList, path1, path2;
    [SerializeField] private GameObject credits;

    /// <summary>
    /// the current wave number
    /// </summary>
    [SerializeField] private int wave;
    private CameraManager _CameraManager;
    private GameData _GameData;

    public int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        _GameData = GameObject.Find("GameData").GetComponent<GameData>();
        _CameraManager = GameObject.Find("MainCamera").GetComponent<CameraManager>();
        wave = 0;
    }

    /// <summary>
    /// Begins the wave coroutine
    /// </summary>
    public void StartWave()
    {
        StartCoroutine(UnleashWave());
    }


    /// <summary>
    /// The wave coroutine that knows which enemies to release and when
    /// </summary>
    /// <returns> an IEnumerator </returns>
    public IEnumerator UnleashWave()
    {
        wave++;
        if(wave == 1)
        {
            _CameraManager.PlayTrack(1);
            numEnemies = 3;
            GameObject bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            yield return SpawnEnemies(0, path2, 0, 1, true);
        }
        else if (wave == 2)
        {
            _CameraManager.PlayTrack(1);
            numEnemies = 9;
            GameObject bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return SpawnEnemies(1, path2, 1, 2, true);
            yield return new WaitForSeconds(4);
            yield return SpawnEnemies(0, path1, 1, 2, true);
            yield return new WaitForSeconds(4);
            yield return SpawnEnemies(0, path2, 1, 4, true);
        }
        else if (wave == 3)
        {
            _CameraManager.PlayTrack(1);
            numEnemies = 16;
            GameObject bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(3f);
            yield return SpawnEnemies(0, path1, 0.5f, 2, false);
            yield return SpawnEnemies(0, path2, 0.5f, 1, false);
            yield return SpawnEnemies(1, path2, 0.5f, 2, false);
            yield return SpawnEnemies(1, path2, 1, 2, false);
            yield return SpawnEnemies(0, path2, 1, 3, false);
            yield return SpawnEnemies(0, path2, 0.5f, 3, false);
            yield return SpawnEnemies(0, path1, 0.5f, 2, true);
        }
        else if (wave == 4)
        {
            _CameraManager.PlayTrack(2);
            numEnemies = 63;
            yield return SpawnEnemies(0, path2, 0.5f, 7, false);
            yield return SpawnEnemies(0, path2, 5, 1, false);
            yield return SpawnEnemies(1, path2, 0, 1, false);
            yield return SpawnEnemies(0, path1, 1, 1, false);
            yield return SpawnEnemies(1, path2, 0, 1, false);

            yield return SpawnEnemies(0, path1, 1, 1, false);
            yield return SpawnEnemies(1, path2, 0, 1, false);
            yield return SpawnEnemies(0, path1, 1, 1, false);
            yield return SpawnEnemies(1, path2, 2, 1, false);
            yield return SpawnEnemies(0, path2, 1, 1, false);

            yield return SpawnEnemies(1, path2, 0, 1, false);
            yield return SpawnEnemies(0, path2, 1, 3, false);
            yield return SpawnEnemies(0, path1, 1, 1, false);
            yield return SpawnEnemies(0, path1, 0, 1, false);
            yield return SpawnEnemies(1, path2, 1, 1, false);
            yield return SpawnEnemies(0, path1, 0, 1, false);

            yield return SpawnEnemies(1, path2, 1, 1, false);
            yield return SpawnEnemies(0, path1, 1, 2, true);
            yield return SpawnEnemies(0, path2, 1, 1, false);
            yield return SpawnEnemies(0, path1, 0, 1, false);
            yield return SpawnEnemies(0, path2, 10, 1, false);

            yield return SpawnEnemies(1, path2, 0.2f, 10, true);
            yield return new WaitForSeconds(2);
            yield return SpawnEnemies(0, path1, 0, 1, false);
            yield return SpawnEnemies(0, path2, 0.2f, 1, false);
            yield return SpawnEnemies(0, path1, 0, 1, false);
            yield return SpawnEnemies(0, path2, 0.2f, 1, false);

            yield return SpawnEnemies(0, path1, 0, 1, false);
            yield return SpawnEnemies(0, path2, 0.2f, 1, false);
            yield return SpawnEnemies(0, path1, 0, 1, false);
            yield return SpawnEnemies(0, path2, 0.2f, 1, false);
            yield return SpawnEnemies(0, path1, 0, 1, false);

            yield return SpawnEnemies(0, path2, 0.2f, 1, false);
            yield return SpawnEnemies(0, path1, 0, 1, false);
            yield return SpawnEnemies(0, path2, 0.2f, 12, true);
        }
        else if(wave == 5)
        {
            _CameraManager.PlayTrack(4);
            credits.SetActive(true);
        }
    }

    public IEnumerator SpawnEnemies(int enemyID, GameObject[] _path, float spacing, int count, bool isFinal)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bot = Instantiate(enemyList[enemyID], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(_path);
            if (!isFinal || i != count - 1)
                yield return new WaitForSeconds(spacing);
            else { yield return new WaitForSeconds(0); }
        }
    }

    /// <summary>
    /// Resets game after the player loses
    /// </summary>
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// @author Addison Shuppy
/// </summary>
public class SpawnManager0 : MonoBehaviour
{

    /// <summary>
    /// The list of all enemies in the game
    /// </summary>
    [SerializeField] private GameObject[] enemyList, path;

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
        if (wave == 1)
        {
            _GameData.AddToArchive(enemyList[0].GetComponent<SpriteRenderer>().sprite, "These robots were meant to clean house." +
                "  Nobody ever thought they could go rogue.  Health: 60.  Speed: 2.  GigaByte Reward: 13.  Damage: 1");
            _CameraManager.PlayTrack(1);
            numEnemies = 5;
            yield return SpawnEnemies(0, path, 1.5f, 1, false);
            yield return SpawnEnemies(0, path, 3, 1, false);
            yield return SpawnEnemies(0, path, 1, 3, true);
        }
        else if (wave == 2)
        {
            _GameData.AddToArchive(enemyList[1].GetComponent<SpriteRenderer>().sprite, "Human sized, but not just any human size." +
                "  Just as large as people who performed physical labor used to be. Hubba hubba." +
                "  Health: 140.  Speed: 1.5.  GigaByte Reward: 25.  Damage: 3");
            _CameraManager.PlayTrack(1);
            numEnemies = 15;
            yield return SpawnEnemies(0, path, 1.5f, 1, false);
            yield return SpawnEnemies(1, path, 2, 1, false);
            yield return SpawnEnemies(0, path, 1, 2, false);
            yield return SpawnEnemies(0, path, 5, 1, false);

            yield return SpawnEnemies(0, path, 0.5f, 4, false);
            yield return SpawnEnemies(1, path, 0.5f, 3, false);
            yield return SpawnEnemies(0, path, 0.5f, 3, true);
        }
        else if (wave == 3)
        {
            _CameraManager.PlayTrack(2);
            numEnemies = 18;

            yield return SpawnEnemies(0, path, 3, 1, false);
            yield return SpawnEnemies(0, path, 0.3f, 2, false);
            yield return SpawnEnemies(0, path, 2, 1, false);
            yield return SpawnEnemies(1, path, 0.5f, 6, false);
            yield return SpawnEnemies(0, path, 0.3f, 1, false);
            yield return SpawnEnemies(0, path, 2, 1, false);
            yield return SpawnEnemies(0, path, 0.3f, 6, true);
        }
        else
        {
            GameData.level++;
            SceneManager.LoadScene(2);
        }
    }

    public IEnumerator SpawnEnemies(int enemyID, GameObject[] _path, float spacing, int count, bool isFinal)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bot = Instantiate(enemyList[enemyID], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(_path);
            if(!isFinal || i != count -1)
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

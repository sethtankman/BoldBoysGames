﻿using System.Collections;
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

    /// <summary>
    /// the current wave number
    /// </summary>
    [SerializeField] private int wave;
    private CameraManager _CameraManager;
    private GameData _GameData;

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
            _GameData.AddToArchive(enemyList[0].GetComponent<SpriteRenderer>().sprite, "These robots were meant to clean house." +
                "  Nobody ever thought they could go rogue.  Health: 60.  Speed: 2.  GigaBytes: 10.  Damage: 1");
            _GameData.AddToArchive(enemyList[1].GetComponent<SpriteRenderer>().sprite, "Human sized, but not just any human size." +
                "  Just as large as people who performed physical labor used to be. Hubba hubba." +
                "  Health: 140.  Speed: 1.5.  GigaBytes: 25.  Damage: 3");
            _CameraManager.PlayTrack(1);
            GameObject bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
        }
        else if (wave == 2)
        {
            _CameraManager.PlayTrack(1);
            GameObject bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(4);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(4);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
        }
        else if (wave == 3)
        {
            _CameraManager.PlayTrack(1);
            GameObject bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(3f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1f);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
        }
        else if (wave == 4)
        {
            _CameraManager.PlayTrack(2);
            GameObject bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(0.5f);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(5);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[1], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(2);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path1);
            bot = Instantiate(enemyList[0], Vector3.zero, Quaternion.identity);
            bot.GetComponent<EnemyBot>().SetPath(path2);
            yield return new WaitForSeconds(1);


        }
    }
}

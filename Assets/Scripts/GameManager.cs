using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
  private const int MAX_ORB = 10;
  private const int START_ORB = 5;
  private const int RESPAWN_TIME = 5;

  public GameObject orbPrefab;
  public GameObject canvasGame;
  public GameObject textScore;

  private int score = 0;
  private int nextScore = 100;
  private int currentOrb = 0;
  private DateTime lastRespawnTime;

  void Start()
  {
    currentOrb = START_ORB;
    for (int i = 0; i < currentOrb; i++)
    {
      CreateOrb();
    }
    lastRespawnTime = DateTime.UtcNow;
    RefreshScoreText();
  }

  // Update is called once per frame
  void Update()
  {
    if (currentOrb < MAX_ORB)
    {
      TimeSpan timeSpan = DateTime.UtcNow - lastRespawnTime;
      if (timeSpan >= TimeSpan.FromSeconds(RESPAWN_TIME))
      {
        while (timeSpan >= TimeSpan.FromSeconds(RESPAWN_TIME))
        {
          CreateNewOrb();
          timeSpan -= TimeSpan.FromSeconds(RESPAWN_TIME);
        }
      }
    }

  }

  public void CreateNewOrb()
  {
    lastRespawnTime = DateTime.UtcNow;
    if (currentOrb >= MAX_ORB)
    {
      return;
    }
    CreateOrb();
    currentOrb++;
  }
  public void CreateOrb()
  {
    GameObject orb = (GameObject)Instantiate(orbPrefab);
    orb.transform.SetParent(canvasGame.transform, false);
    orb.transform.localPosition = new Vector3(
        UnityEngine.Random.Range(-300.0f, 300.0f),
        UnityEngine.Random.Range(-140.0f, -500.0f),
        0f
    );
  }

  public void GetOrb()
  {
    score++;
    RefreshScoreText();
    currentOrb--;
  }
  void RefreshScoreText()
  {
    textScore.GetComponent<Text>().text = "徳： " + score + " / " + nextScore;
  }
}

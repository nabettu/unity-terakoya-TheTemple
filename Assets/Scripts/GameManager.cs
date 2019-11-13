using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
  private const int MAX_ORB = 10;
  private const int START_ORB = 5;
  private const int RESPAWN_TIME = 1;
  private const int MAX_LEVEL = 2;

  public GameObject orbPrefab;
  public GameObject smokePrefab;
  public GameObject kusudamaPrefab;
  public GameObject canvasGame;
  public GameObject textScore;
  public GameObject imageTemple;

  private int score = 0;
  private int nextScore = 10;
  private int currentOrb = 0;
  private int templeLevel = 0;
  private Boolean isCleared = false;
  private DateTime lastRespawnTime;
  private int[] nextScoreTable = new int[] { 10, 10, 10 };
  void Start()
  {
    currentOrb = START_ORB;
    for (int i = 0; i < currentOrb; i++)
    {
      CreateOrb();
    }
    lastRespawnTime = DateTime.UtcNow;

    nextScore = nextScoreTable[templeLevel];
    imageTemple.GetComponent<TempleManager>().SetTemplePicture(templeLevel);
    imageTemple.GetComponent<TempleManager>().SetTempleScale(score, nextScore);
    RefreshScoreText();
  }

  // Update is called once per frame
  void Update()
  {
    if (currentOrb < MAX_ORB && !isCleared)
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
    CreateOrb();
    CreateOrb();
    currentOrb += 3;
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
    if (score > nextScore)
    { score = nextScore; }
    TempleLevelUpCheck();
    RefreshScoreText();
    imageTemple.GetComponent<TempleManager>().SetTempleScale(score, nextScore);
    if ((score == nextScore) && (templeLevel == MAX_LEVEL))
    {
      ClearEffect();
      isCleared = true;
    }


    currentOrb--;
  }
  void RefreshScoreText()
  {
    textScore.GetComponent<Text>().text = "徳： " + score + " / " + nextScore + " : 寺レベル" + (templeLevel + 1);
  }

  void TempleLevelUpCheck()
  {
    if (score >= nextScore)
    {
      if (templeLevel < MAX_LEVEL)
      {
        templeLevel++;
        score = 0;
        TempleLevelUpEffect();
        nextScore = nextScoreTable[templeLevel];
        imageTemple.GetComponent<TempleManager>().SetTemplePicture(templeLevel);
      }
    }
  }
  void TempleLevelUpEffect()
  {
    GameObject smoke = (GameObject)Instantiate(smokePrefab);
    smoke.transform.SetParent(canvasGame.transform, false);
    smoke.transform.SetSiblingIndex(2);
    Destroy(smoke, 0.5f);
  }

  void ClearEffect()
  {
    GameObject kusudama = (GameObject)Instantiate(kusudamaPrefab);
    kusudama.transform.SetParent(canvasGame.transform, false);
  }
}

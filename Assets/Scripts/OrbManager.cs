﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class OrbManager : MonoBehaviour
{

  public Sprite[] orbPicture = new Sprite[3];

  public enum ORB_KIND { BLUE, GREEN, PURPLE };
  public ORB_KIND orbKind;
  private GameObject gameManager;
  // Start is called before the first frame update
  void Start()
  {
    gameManager = GameObject.Find("GameManager");
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void TouchOrb()
  {
    if (Input.GetMouseButton(0) == false)
    {
      return;
    }
    switch (orbKind)
    {
      case ORB_KIND.BLUE:
        gameManager.GetComponent<GameManager>().GetOrb(1);
        break;
      case ORB_KIND.GREEN:
        gameManager.GetComponent<GameManager>().GetOrb(5);
        break;
      case ORB_KIND.PURPLE:
        gameManager.GetComponent<GameManager>().GetOrb(10);
        break;

    }
    Destroy(this.gameObject);
  }

  public void SetKind(ORB_KIND kind)
  {
    orbKind = kind;
    switch (orbKind)
    {
      case ORB_KIND.BLUE:
        GetComponent<Image>().sprite = orbPicture[0];
        break;
      case ORB_KIND.GREEN:
        GetComponent<Image>().sprite = orbPicture[1];
        break;
      case ORB_KIND.PURPLE:
        GetComponent<Image>().sprite = orbPicture[2];
        break;

    }

  }

}

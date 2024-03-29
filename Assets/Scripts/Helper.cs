﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Helper : MonoBehaviour, IPointerEnterHandler {

    float clickSpeed = 1.8f;
    public Pixel myPixel;
 

	// Use this for initialization
	void Start () {
        if (GameManager.instance.phase != 6) {
            Vector3 destination = myPixel.transform.position + new Vector3(60, -50, 0);
            transform.DOMove(destination, 2).OnComplete(StartClicking);
        }
        else {
            StartClickingPhase6();
        }
	}

    void StartClickingPhase6() {
        InvokeRepeating("Click", 1.5f, clickSpeed);
    }
        
    public void StartClicking() {
        InvokeRepeating("Click", 0.1f, clickSpeed);
    }

    void Click() {
        myPixel.UnFadeByHelper();
    }

    private void Update()
    {
        if (GameManager.instance.phase == 5)
        {
            CancelInvoke();
            Vector3 mouseDest = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            transform.DOMove(mouseDest, 0.5f);
            
        }
        else if (GameManager.instance.isGameEnd) {
            CancelInvoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.instance.phase == 5)
        {
            gameObject.SetActive(false);
        }
    }
}

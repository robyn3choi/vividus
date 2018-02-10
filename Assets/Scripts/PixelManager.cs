﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelManager : MonoBehaviour {

    public static PixelManager instance = null;
   
    List<Pixel> pixels = new List<Pixel>();
    public float timeBetweenPixelFades = 1; // will gradually decrease by GameManager
    public float fadeDur = 3;
    float fadeSpeedupRate = 0.05f;
    float fadeSpeedupAccelRate = 0.05f;
    float timer = 0;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null) {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this) {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        }
    }
        
	void Start () {
        // get all the Pixel components in my children and add to my pixel list
        foreach (Transform t in transform) {
            Pixel pixel = t.GetComponent<Pixel>();
            pixels.Add(pixel);
        }
	}

    void Update() {
        if (GameManager.instance.phase < 3) {
            FadeRandomPixels();
            SpeedUpFading();
        }

        if (GameManager.instance.phase == 2) {
            fadeSpeedupRate += fadeSpeedupAccelRate * Time.deltaTime;
        }
    }

    void FadeRandomPixels() {
        if (timer >= timeBetweenPixelFades) {
            Pixel randomPixel = GetRandomPixel();
            if (!randomPixel.isFading) {
                randomPixel.isFading = true;
            }
            timer = 0;
        }
        else {
            timer += Time.deltaTime;
        }
    }

    void SpeedUpFading() {
        //fadeDur -= fadeSpeedupRate * Time.deltaTime;
        timeBetweenPixelFades -= fadeSpeedupRate * Time.deltaTime;
    }

    Pixel GetRandomPixel() {
        int randomIndex = Random.Range(0, pixels.Count);
        return pixels[randomIndex];
    }
}

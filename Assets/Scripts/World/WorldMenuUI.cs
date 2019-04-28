﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMenuUI : MonoBehaviour
{
    MenuManager mm;
    WorldManager wm;

    void Start()
    { 
        mm = transform.root.GetChild(5).GetComponent<MenuManager>();
        wm = transform.root.GetChild(5).GetComponent<WorldManager>();

        //Listeners for buttons.
        Button dupBtn = transform.GetChild(4).GetComponent<Button>();
        dupBtn.onClick.AddListener(DuplicateButtonPressed);

        Button renameBtn = transform.GetChild(3).GetComponent<Button>();
        renameBtn.onClick.AddListener(RenameButtonPressed);

        Button playBtn = transform.GetChild(2).GetComponent<Button>();
        playBtn.onClick.AddListener(PlayButtonPressed);

        Button delBtn = transform.GetChild(1).GetComponent<Button>();
        delBtn.onClick.AddListener(DeleteButtonPressed);
    }

    //Button Listener functions
    void DeleteButtonPressed()
    {
        Destroy(gameObject);
        wm.SaveWorlds();
    }

    void PlayButtonPressed()
    {
        wm.SaveWorlds();
        mm.LoadWorld(gameObject.name, WorldData.currentSeed);
    }

    void DuplicateButtonPressed()
    {

    }

    void RenameButtonPressed()
    {

    }
}

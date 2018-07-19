using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.InputModule;

public class AirTapGesture : MonoBehaviour, IInputClickHandler
{
    public GameObject original;

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
       // gameObject.AddComponent<Rigidbody>();

        GameObject cube = GameObject.Instantiate(original);
        cube.transform.position = Camera.main.transform.TransformPoint(0, 0, 1.2f);
    }
}
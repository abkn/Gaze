using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using VRStandardAssets.Utils;

public class GazeInputTarget : MonoBehaviour, IFocusable
{

    public GameObject Explosion;
    // public AudioSource AS;
    public AudioClip sound01;
    public AudioClip sound02;

    public void OnFocusEnter()
    {
        //オブジェクトに視線が当たったらゲージがいっぱいになった時のイベントを登録
        SelectionRadial.Instance.OnSelectionComplete += OnSelectionComplete;
        SelectionRadial.Instance.Show();
        SelectionRadial.Instance.HandleDown();
        GetComponent<AudioSource>().PlayOneShot(sound02);
    }

    public void OnFocusExit()
    {
        //オブジェクトに視線がはずれたらゲージがいっぱいになった時のイベントを解除
        SelectionRadial.Instance.OnSelectionComplete -= OnSelectionComplete;
        SelectionRadial.Instance.HandleUp();
        SelectionRadial.Instance.Hide();

    }

    private void OnSelectionComplete()
    {
        //ここにゲージがいっぱいになったら実行される処理を書く。今回は色を青に変更
        //gameObject.GetComponent<Renderer>().material.color = Color.blue;

        AudioSource.PlayClipAtPoint(sound01, transform.position);
        Destroy(this.gameObject);

        Instantiate(Explosion, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

        SelectionRadial.Instance.OnSelectionComplete -= OnSelectionComplete;
        SelectionRadial.Instance.HandleUp();
        SelectionRadial.Instance.Hide();
    }
}

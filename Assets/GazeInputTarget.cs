using UnityEngine;
using HoloToolkit.Unity.InputModule;
using VRStandardAssets.Utils;

public class GazeInputTarget : MonoBehaviour, IFocusable
{

    public GameObject Explosion;
    public AudioClip sound01;
    public AudioClip sound02;

    public void OnFocusEnter()
    {
        SelectionRadial.Instance.OnSelectionComplete += OnSelectionComplete;
        SelectionRadial.Instance.Show();
        SelectionRadial.Instance.HandleDown();
        GetComponent<AudioSource>().PlayOneShot(sound02);
    }

    public void OnFocusExit()
    {
        SelectionRadial.Instance.OnSelectionComplete -= OnSelectionComplete;
        SelectionRadial.Instance.HandleUp();
        SelectionRadial.Instance.Hide();

    }

    private void OnSelectionComplete()
    {
        AudioSource.PlayClipAtPoint(sound01, transform.position);
        Destroy(this.gameObject);

        Instantiate(Explosion, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

        SelectionRadial.Instance.OnSelectionComplete -= OnSelectionComplete;
        SelectionRadial.Instance.HandleUp();
        SelectionRadial.Instance.Hide();
    }
}

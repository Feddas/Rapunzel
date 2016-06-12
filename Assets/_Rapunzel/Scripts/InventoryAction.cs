using UnityEngine;
using System.Collections;

public class InventoryAction : MonoBehaviour
{
    [Tooltip("Only needs to be set if PlayAudio() is used")]
    public AudioSource NoiseMaker;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    #region [ InventoryEvent.cs UnityEvent handlers ]
    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        NoiseMaker.PlayOneShot(audioClip);
    }

    /// <summary> SetActive to false for the given gameobject in 2 frames. These 2 frames gives Inventory.cs a chance on it's schedule of merging inventory lists in 1 frame. </summary>
    /// <param name="targetGameObject"></param>
    public void SetActiveFalse(GameObject targetGameObject)
    {
        StartCoroutine(SetActiveIn2Frames(targetGameObject, false));
    }

    public IEnumerator SetActiveIn2Frames(GameObject targetGameObject, bool isActive)
    {
        yield return null;
        yield return null;

        targetGameObject.SetActive(isActive);
    }

    public void CenterOnParent(Transform target)
    {
        target.localPosition = Vector3.zero;
    }
    #endregion [ InventoryEvent.cs UnityEvent handlers ]
}

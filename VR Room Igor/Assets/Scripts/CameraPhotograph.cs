using UnityEngine;

public class CameraPhotograph : MonoBehaviour
{
    [SerializeField] GameObject photographOutput;

    public void StartPhoto(Texture2D image)
    {
        GetComponent<Rigidbody>().isKinematic = true;

        photographOutput.GetComponent<Renderer>().material.mainTexture = image;
    }

    public void StopPhoto()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}

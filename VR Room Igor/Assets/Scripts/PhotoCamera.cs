using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PhotoCamera : XRGrabInteractable
{
    [SerializeField] private Camera cameraView;
    [SerializeField] private GameObject photoPrefab;
    [SerializeField] private Transform exitPoint;

    private AudioSource audioSource;

    private Vector3 targetPosition;
    private float targetDistance = 0.1f;
    private GameObject currentPhoto;

    
    
    Texture2D GetCameraImage(Camera camera)
    {
        // https://docs.unity3d.com/ScriptReference/Camera.Render.html

        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        activated.AddListener(OnActivate);
    }

    private void OnActivate(ActivateEventArgs arg0)
    {
        audioSource.Play();

        Texture2D image = GetCameraImage(cameraView);
        currentPhoto = Instantiate(photoPrefab, exitPoint.position, exitPoint.rotation);
        currentPhoto.GetComponent<CameraPhotograph>().StartPhoto(image);

    }

    private void Update()
    {
        if (currentPhoto != null)
        {
            targetPosition = exitPoint.position + exitPoint.forward * targetDistance;
            currentPhoto.transform.position = Vector3.Lerp(currentPhoto.transform.position, targetPosition, 0.02f * Time.deltaTime);
            if (Vector3.Distance(currentPhoto.transform.position, targetPosition) > targetDistance)
            {
                currentPhoto.GetComponent<CameraPhotograph>().StopPhoto();
                currentPhoto = null;
            }
        }
    }
}

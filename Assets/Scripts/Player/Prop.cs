using UnityEngine;

public class Prop : Role
{
    [Header("References")]
    [SerializeField] private Transform propVisualParent;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Collider propCollider;

    private CameraController cameraController;
    private Rigidbody rb;
    private Player player;

    private PropData[] propOptions;
    private int currentIndex = 0;
    private GameObject currentModel;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetCameraMode(CameraController.CameraMode.FirstPerson);

        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();

        if (propCollider != null)
            propCollider.enabled = false;
    }

    /// <summary>
    /// Initialize the list of props this player can switch between.
    /// </summary>
    public void InitializeWithProps(PropData[] props)
    {
        propOptions = props;

        if (propOptions != null && propOptions.Length > 0)
        {
            currentIndex = 0;
            SetProp(propOptions[currentIndex]);
        }
        else
        {
            Debug.LogWarning("No PropData provided to InitializeWithProps.");
        }
    }

    public void SetVisualParent(Transform parent)
    {
        propVisualParent = parent;
    }

    public void SetCollider(Collider col)
    {
        propCollider = col;
    }

    /// <summary>
    /// Switches to the given prop and applies movement settings and visuals.
    /// </summary>
    private void SetProp(PropData data)
    {
        // Destroy old model
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // Instantiate new model
        if (data.propPrefab != null && propVisualParent != null)
        {
            currentModel = Instantiate(data.propPrefab, propVisualParent);
            currentModel.transform.localPosition = Vector3.zero;
            currentModel.transform.localRotation = Quaternion.identity;
        }

        // Apply movement settings to the player
        if (player != null)
        {
            player.jumpForce = data.jumpHeight;
            player.speed = data.moveSpeed;
        }
    }

    public override void FirstAction()
    {
        if (propOptions == null || propOptions.Length == 0) return;

        currentIndex = (currentIndex + 1) % propOptions.Length;
        SetProp(propOptions[currentIndex]);
    }

    public override void SecondAction()
    {
        if (propOptions == null || propOptions.Length == 0) return;

        currentIndex = (currentIndex - 1 + propOptions.Length) % propOptions.Length;
        SetProp(propOptions[currentIndex]);
    }

    public override void ThirdAction()
    {
        cameraController.ToggleCameraMode();
    }

    public void OnHit()
    {
        if (_particleSystem != null)
        {
            ParticleSystem particleSystem = Instantiate(_particleSystem, transform.position, transform.rotation);
            particleSystem.Play();
        }

        if (propOptions != null && propOptions.Length > 0 && propOptions[currentIndex].deathSound != null)
        {
            AudioSource.PlayClipAtPoint(propOptions[currentIndex].deathSound, transform.position);
        }

        Destroy(gameObject);
        Debug.Log("Prop Hit");
    }
}
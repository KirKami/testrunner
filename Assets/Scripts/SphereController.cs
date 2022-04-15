using UnityEngine;

public class SphereController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    [Tooltip("Sphere controls using physics force. Fmove = Faccel - Finertia")]
    public float acceleration = 100f;
    #endregion
    #region PRIVATE_VARIABLES
    private Rigidbody rigid;
    private Transform MainCamera;
    #endregion

    #region UNITY_CALLBACKS
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        if (Camera.main != null)
            MainCamera = Camera.main.transform;
        else
            Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
    }
    // Update is called once per frame
    void Update()
    {
        //Do nothing if there is no camera
        if (MainCamera == null) return; 
        
        //Get Input Values and Gravity
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var g = 10.0f;

        //Get Input Values relative to camera
        var camForward = Vector3.Scale(MainCamera.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward + h * MainCamera.right;

        //Apply acceleration force
        rigid.AddForce(move * acceleration * Time.fixedDeltaTime,ForceMode.Force);
        //Apply inertia force
        rigid.AddForce(-rigid.velocity, ForceMode.Force);
    }
    #endregion

    #region PUBLIC_METHODS
    public void FreezeSphere()
    {
        rigid.isKinematic = true;
        rigid.velocity = Vector3.zero;
    }
    public void UnfreezeSphere()
    {
        rigid.isKinematic = false;
    }
    #endregion
    
    #region PRIVATE_METHODS
    #endregion
}

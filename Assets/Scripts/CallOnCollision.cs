using UnityEngine;
using UnityEngine.Events;

public class CallOnCollision : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    #endregion
    #region PRIVATE_VARIABLES
    [SerializeField] private UnityEvent callOnCollision;
    #endregion
    #region UNITY_CALLBACKS
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) callOnCollision.Invoke();
    }
    #endregion
    #region PUBLIC_METHODS
    #endregion
    #region PRIVATE_METHODS
    #endregion
}

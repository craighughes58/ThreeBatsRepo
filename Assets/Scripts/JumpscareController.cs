using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpscareController : MonoBehaviour
{
    #region Serialized Variables

    [Tooltip("The images that show up when jumpscares happen")]
    [SerializeField] private List<Image> _jumpScares;
    
    #endregion

    #region Private Variables

    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ActivateJumpscare()
    {
        
    }

    private IEnumerator JumpscareCoroutine()
    {
        yield return new WaitForEndOfFrame();
    }
}

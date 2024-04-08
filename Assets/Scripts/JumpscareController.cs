using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpscareController : MonoBehaviour
{
    #region Serialized Variables

    [Tooltip("The images that show up when jumpscares happen")]
    [SerializeField] private List<Image> _jumpScares;

    [Tooltip("How fast the jumpscare fades away")]
    [SerializeField] private float _dissolveSpeed;
    
    #endregion

    #region Private Variables

    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ActivateJumpscare()
    {
        StartCoroutine(JumpscareCoroutine());
    }

    private IEnumerator JumpscareCoroutine()
    {
        int randomJumpscare = Random.Range(0,_jumpScares.Count);
        _jumpScares[randomJumpscare].enabled = true;
        _jumpScares[randomJumpscare].color = new Color(1,1,1,1);
        while (_jumpScares[randomJumpscare].color.a > 0)
        {
            _jumpScares[randomJumpscare].color = new Color(1, 1, 1, 1);
            yield return new WaitForEndOfFrame();
        }
    }
}

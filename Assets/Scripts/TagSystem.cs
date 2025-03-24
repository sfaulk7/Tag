using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class TagSystem : MonoBehaviour
{
    [SerializeField]
    private bool _startTagged = false;

    [SerializeField]
    private float _tagImmunityDuration = 1.0f;

    private bool _tagged = false;
    private bool _tagImmune = false;

    public bool Tagged { get { return _tagged; } }

    public bool Tag()
    {
        //If already it ignore
        if (Tagged)
            return false;

        //If immune to tag, don't
        if (_tagImmune) return false;

        //Set the new tagged
        _tagged = true;
        return true;
    }

    private void SetTagImmuneFalse()
    {
        _tagImmune = false;
    }

    private void Start()
    {
        _tagged = _startTagged;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If we arent it ignore
        if (!(_tagged))
            return;

        if(collision.gameObject.TryGetComponent(out TagSystem tagSystem))
        {
            if(tagSystem.Tag())
            {
                //Test
                if(collision.gameObject.TryGetComponent(out PlayerController controller))
                {
                    string other = "";
                    string me = "";
                }
                _tagged = false;
                _tagImmune = true;
                Invoke(nameof(SetTagImmuneFalse), _tagImmunityDuration);
            }
        }
    }
}

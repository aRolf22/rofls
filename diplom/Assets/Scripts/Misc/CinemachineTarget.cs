using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CinemachineTarget : MonoBehaviour
{
    public bool ThisIsAndroidBuild = false;
    public bool offCursorForAndroid;

    private CinemachineTargetGroup cinemachineTargetGroup;

    #region Tooltip
    [Tooltip("Populate with the CursorTarget gameobject")]
    #endregion Tooltip
    [SerializeField] private Transform cursorTarget;
    public Image cursorImage;

    private void Awake()
    {
        // Load components
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCinemachineTargetGroup();

        if (offCursorForAndroid)
        {
            cursorImage.enabled = false;
        }
    }

    /// <summary>
    /// Set the cinemachine camera target group.
    /// </summary>
    private void SetCinemachineTargetGroup()
    {
        // Create target group for cinemachine for the cinemachine camera to follow  - group will include the player and screen cursor
        CinemachineTargetGroup.Target cinemachineGroupTarget_player = new CinemachineTargetGroup.Target { Weight = 1f, Radius = 5.5f, Object = GameManager.Instance.GetPlayer().transform };

        CinemachineTargetGroup.Target cinemachineGroupTarget_cursor = new CinemachineTargetGroup.Target { Weight = 1f, Radius = 1f, Object = cursorTarget };

        if (ThisIsAndroidBuild)
        {
            // Если это андройд-билд, то курсор нам не нужен. Надо чтобы камеры была только на игроке
            cinemachineTargetGroup.Targets = new List<CinemachineTargetGroup.Target> { cinemachineGroupTarget_player };
        }
        else
        {
            cinemachineTargetGroup.Targets = new List<CinemachineTargetGroup.Target> { cinemachineGroupTarget_player, cinemachineGroupTarget_cursor };
        }
        
    }

    private void Update()
    {
        cursorTarget.position = HelperUtilities.GetMouseWorldPosition();
    }

}

using System.Collections.Generic;
using UnityEngine;

public interface IDoubleRuneAction
{
    void TriggerDown(PlayerController player);
    void TriggerUp();
}

[RequireComponent(typeof(PlayerController))]
public class DoubleRuneActionController : MonoBehaviour
{
    [SerializeField] private PickupController pickupController;
    private PlayerController playerController;
    [SerializeField] private GameObject boobyTrapPrefab;
    [SerializeField] private GameObject laserAttackPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject slowFieldPrefab;
    [SerializeField] private GameObject magnetPrefab;

    private Dictionary<ActionType, IDoubleRuneAction> actions;
    private IDoubleRuneAction currentAction;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        actions = new Dictionary<ActionType, IDoubleRuneAction>
        {
            { ActionType.Trap,   InstantiateAction<BoobyTrap>(boobyTrapPrefab) },
            { ActionType.Laser,  InstantiateAction<LaserAttack>(laserAttackPrefab) },
            { ActionType.Shield, InstantiateAction<Shield>(shieldPrefab) },
            { ActionType.Slow,   InstantiateAction<SlowField>(slowFieldPrefab) },
            { ActionType.Magnet, InstantiateAction<MagnetController>(magnetPrefab) },
        };
    }

    private T InstantiateAction<T>(GameObject prefab) where T : MonoBehaviour, IDoubleRuneAction
    {
        if (prefab == null)
        {
            Debug.LogError($"[DoubleRuneActionController] Prefab for {typeof(T).Name} is not assigned in the inspector.");
            return null;
        }
        var instance = Instantiate(prefab);
        var component = instance.GetComponent<T>();
        if (component == null)
            Debug.LogError($"[DoubleRuneActionController] Prefab '{prefab.name}' is missing the {typeof(T).Name} component.");
        return component;
    }

    void Update()
    {
        var gamepad = playerController.assignedGamepad;
        if (gamepad == null) return;

        if (gamepad.rightTrigger.wasPressedThisFrame)
            TriggerDown();
        else if (gamepad.rightTrigger.wasReleasedThisFrame)
            TriggerUp();
    }

    public void TriggerDown()
    {
        if (!pickupController.TryPeekPair(out RuneType first, out RuneType second))
            return;

        ActionType actionType = RuneToAction.GetAction(first, second);
        if (!actions.TryGetValue(actionType, out currentAction)) return;
        currentAction.TriggerDown(playerController);
    }

    public void TriggerUp()
    {
        if (currentAction == null) return;
        currentAction.TriggerUp();
        currentAction = null;
        pickupController.ExpendPair();
    }
}

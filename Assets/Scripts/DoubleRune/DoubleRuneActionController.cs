using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IDoubleRuneAction
{
    void TriggerDown();
    void TriggerUp();
}

public class DoubleRuneActionController : MonoBehaviour
{
    [SerializeField] private PickupController pickupController;
    [SerializeField] private GameObject boobyTrapPrefab;
    [SerializeField] private GameObject laserAttackPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject slowFieldPrefab;

    private Dictionary<ActionType, IDoubleRuneAction> actions;
    private IDoubleRuneAction currentAction;

    void Awake()
    {
        actions = new Dictionary<ActionType, IDoubleRuneAction>
        {
            { ActionType.Trap,   InstantiateAction<BoobyTrap>(boobyTrapPrefab) },
            { ActionType.Laser,  InstantiateAction<LaserAttack>(laserAttackPrefab) },
            { ActionType.Shield, InstantiateAction<Shield>(shieldPrefab) },
            { ActionType.Slow,   InstantiateAction<SlowField>(slowFieldPrefab) },
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
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        if (gamepad.rightTrigger.wasPressedThisFrame)
            TriggerDown();
        else if (gamepad.rightTrigger.wasReleasedThisFrame)
            TriggerUp();
    }

    public void TriggerDown()
    {
        if (!pickupController.TryPeekPair(out RuneType first, out RuneType second))
        {
            Debug.LogError("[DoubleRuneActionController] Not enough runes in queue to determine action.");
            return;
        }

        ActionType actionType = RuneToAction.GetAction(first, second);
        if (!actions.TryGetValue(actionType, out currentAction)) return;
        currentAction.TriggerDown();
    }

    public void TriggerUp()
    {
        if (currentAction == null) return;
        currentAction.TriggerUp();
        currentAction = null;
        pickupController.ExpendPair();
    }
}

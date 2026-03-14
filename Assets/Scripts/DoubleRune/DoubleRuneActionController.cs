using UnityEngine;
using UnityEngine.InputSystem;

public interface IDoubleRuneAction
{
    void Execute();
}

public class DoubleRuneActionController : MonoBehaviour
{
    [SerializeField] private GameObject boobyTrapPrefab;
    [SerializeField] private GameObject laserAttackPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject slowFieldPrefab;

    private IDoubleRuneAction[] actions;

    void Awake()
    {
        actions = new IDoubleRuneAction[]
        {
            InstantiateAction<BoobyTrap>(boobyTrapPrefab),
            InstantiateAction<LaserAttack>(laserAttackPrefab),
            InstantiateAction<Shield>(shieldPrefab),
            InstantiateAction<SlowField>(slowFieldPrefab)
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
        if (gamepad != null && gamepad.rightTrigger.wasPressedThisFrame)
            TriggerRandomAction();
    }

    public void TriggerRandomAction()
    {
        int index = Random.Range(0, actions.Length);
        actions[index].Execute();
    }
}

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
    [SerializeField] private GameObject slowCircleAttackPrefab;

    private IDoubleRuneAction[] actions;

    void Awake()
    {
        actions = new IDoubleRuneAction[]
        {
            Instantiate(boobyTrapPrefab).GetComponent<BoobyTrap>(),
            Instantiate(laserAttackPrefab).GetComponent<LaserAttack>(),
            Instantiate(shieldPrefab).GetComponent<Shield>(),
            Instantiate(slowCircleAttackPrefab).GetComponent<SlowCircleAttack>()
        };
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

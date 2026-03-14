using UnityEngine;

public interface IDoubleRuneAction
{
    void Execute();
}

public class DoubleRuneActionController : MonoBehaviour
{
    [SerializeField] private BoobyTrap boobyTrap;
    [SerializeField] private LaserAttack laserAttack;
    [SerializeField] private Shield shield;
    [SerializeField] private SlowCircleAttack slowCircleAttack;

    private IDoubleRuneAction[] actions;

    void Awake()
    {
        actions = new IDoubleRuneAction[] { boobyTrap, laserAttack, shield, slowCircleAttack };
    }

    public void TriggerRandomAction()
    {
        int index = Random.Range(0, actions.Length);
        actions[index].Execute();
    }
}

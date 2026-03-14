using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] UI_RuneController[] uiRuneControllers;

    private int playerCount;

    void Start()
    {
        foreach (var gamepad in Gamepad.all)
            SpawnPlayer(gamepad);

        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad gamepad && change == InputDeviceChange.Added)
            SpawnPlayer(gamepad);
    }

    void SpawnPlayer(Gamepad gamepad)
    {
        var player = Instantiate(playerPrefab);
        player.GetComponent<PlayerController>().assignedGamepad = gamepad;

        if (playerCount < uiRuneControllers.Length)
            player.GetComponent<PickupController>().uiRuneController = uiRuneControllers[playerCount];

        playerCount++;
    }
}

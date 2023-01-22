//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/Prefabs/Actors/Player/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""2d58e498-e0d1-4864-aea5-1e20823016a1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""acdec0e2-eb9e-4009-a676-832498ca436b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""badcf7da-0e14-4614-9f80-7016f9fded3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BulletShoot"",
                    ""type"": ""Button"",
                    ""id"": ""e7a027bb-3d7f-4dfc-b62f-cf71d93562c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RopeShoot"",
                    ""type"": ""Button"",
                    ""id"": ""431c39a6-cc7a-4a3b-b6d6-131edc56440f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""9fa202ac-fc0d-47db-b3ac-971f395cc127"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5dcdc670-e678-41f6-9a98-02a575d7e897"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""a6315aa1-55ea-407b-96e0-9141a9dd388a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1557ab1a-c184-48ab-aba8-f6d1658fdcff"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6af8adf7-4366-49b2-932c-1f9c5aad075a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""14d5b452-675d-48df-95fe-f9210d7ee06a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7dea4213-cdd3-45f7-92d8-c14921bdcad8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d371ab4-40ce-42eb-bb16-45023888e0a1"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffb2cbd6-c5ad-454b-9799-c2b19e00b1de"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulletShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69ef9fa9-e6cd-475f-b57d-7c441ee4be34"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RopeShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95fce20e-1171-46c4-b666-481181704a1a"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d192cf0c-da03-4e68-aa61-6349488a6f15"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Rotation = m_Gameplay.FindAction("Rotation", throwIfNotFound: true);
        m_Gameplay_BulletShoot = m_Gameplay.FindAction("BulletShoot", throwIfNotFound: true);
        m_Gameplay_RopeShoot = m_Gameplay.FindAction("RopeShoot", throwIfNotFound: true);
        m_Gameplay_Menu = m_Gameplay.FindAction("Menu", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Rotation;
    private readonly InputAction m_Gameplay_BulletShoot;
    private readonly InputAction m_Gameplay_RopeShoot;
    private readonly InputAction m_Gameplay_Menu;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Rotation => m_Wrapper.m_Gameplay_Rotation;
        public InputAction @BulletShoot => m_Wrapper.m_Gameplay_BulletShoot;
        public InputAction @RopeShoot => m_Wrapper.m_Gameplay_RopeShoot;
        public InputAction @Menu => m_Wrapper.m_Gameplay_Menu;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Rotation.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotation;
                @BulletShoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBulletShoot;
                @BulletShoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBulletShoot;
                @BulletShoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBulletShoot;
                @RopeShoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRopeShoot;
                @RopeShoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRopeShoot;
                @RopeShoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRopeShoot;
                @Menu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @BulletShoot.started += instance.OnBulletShoot;
                @BulletShoot.performed += instance.OnBulletShoot;
                @BulletShoot.canceled += instance.OnBulletShoot;
                @RopeShoot.started += instance.OnRopeShoot;
                @RopeShoot.performed += instance.OnRopeShoot;
                @RopeShoot.canceled += instance.OnRopeShoot;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnBulletShoot(InputAction.CallbackContext context);
        void OnRopeShoot(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
}
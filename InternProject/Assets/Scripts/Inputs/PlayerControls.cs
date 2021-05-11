// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Tank"",
            ""id"": ""7d86de4e-1af8-4938-8e12-a0b92ce41cdd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e4a9fde9-2483-42ad-a068-5a349a91ff92"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""fc483d97-6c2f-476f-8301-eb6bd552ba1b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""392bc23a-aff4-4fc1-aae1-e0cadda75d37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookAt"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a4252ca3-c1f8-47e0-bc45-1c628f253b57"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""b455bf57-062b-47c7-b3e4-77443f19e7d8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""29b3a0ea-48bb-4081-a8f0-d2a592e692c9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6841d03b-8620-4b77-8c78-5fce9d59c080"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""e17b538b-4a18-4038-9e02-a9f434e419a7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bec476e3-d38b-4317-9e45-818bf7d7f705"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""694af0a9-4abc-4ee7-a1e1-1bc48b93b6e0"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""09a4e549-8134-454f-a72f-e9f75ff7a433"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fa65c2cd-cb9b-4d25-b537-b2fb8d33691a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b4a1a253-6a09-4a20-b2ce-e1a517f1defd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""e626f64e-6093-4b0b-ac48-534073eda11a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b5add6d5-002c-4f55-8699-9737ad3a6ff5"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4668f3a0-4859-4c07-bb1a-59da056cc7be"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a7cc1b23-772a-4054-aefe-1352da501500"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4902e15-fdfa-4b65-bd6c-b39902027046"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""839784ef-6d1a-44aa-a140-cd503554adb2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""3ac9e214-ea75-4fd2-8d3a-97fd9ed6790c"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""71e10878-f5cd-4558-aa46-43d16de1663a"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""61741cdf-b075-4ef7-97d9-633dd73636d7"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""802b5cf6-7f12-4fd6-a519-f28fe43ef4a7"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""70007d4f-e140-4296-8a33-d3704c531a6f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player"",
            ""bindingGroup"": ""Player"",
            ""devices"": []
        }
    ]
}");
        // Tank
        m_Tank = asset.FindActionMap("Tank", throwIfNotFound: true);
        m_Tank_Move = m_Tank.FindAction("Move", throwIfNotFound: true);
        m_Tank_Rotate = m_Tank.FindAction("Rotate", throwIfNotFound: true);
        m_Tank_Shoot = m_Tank.FindAction("Shoot", throwIfNotFound: true);
        m_Tank_LookAt = m_Tank.FindAction("LookAt", throwIfNotFound: true);
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

    // Tank
    private readonly InputActionMap m_Tank;
    private ITankActions m_TankActionsCallbackInterface;
    private readonly InputAction m_Tank_Move;
    private readonly InputAction m_Tank_Rotate;
    private readonly InputAction m_Tank_Shoot;
    private readonly InputAction m_Tank_LookAt;
    public struct TankActions
    {
        private @PlayerControls m_Wrapper;
        public TankActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Tank_Move;
        public InputAction @Rotate => m_Wrapper.m_Tank_Rotate;
        public InputAction @Shoot => m_Wrapper.m_Tank_Shoot;
        public InputAction @LookAt => m_Wrapper.m_Tank_LookAt;
        public InputActionMap Get() { return m_Wrapper.m_Tank; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TankActions set) { return set.Get(); }
        public void SetCallbacks(ITankActions instance)
        {
            if (m_Wrapper.m_TankActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_TankActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnRotate;
                @Shoot.started -= m_Wrapper.m_TankActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnShoot;
                @LookAt.started -= m_Wrapper.m_TankActionsCallbackInterface.OnLookAt;
                @LookAt.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnLookAt;
                @LookAt.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnLookAt;
            }
            m_Wrapper.m_TankActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @LookAt.started += instance.OnLookAt;
                @LookAt.performed += instance.OnLookAt;
                @LookAt.canceled += instance.OnLookAt;
            }
        }
    }
    public TankActions @Tank => new TankActions(this);
    private int m_PlayerSchemeIndex = -1;
    public InputControlScheme PlayerScheme
    {
        get
        {
            if (m_PlayerSchemeIndex == -1) m_PlayerSchemeIndex = asset.FindControlSchemeIndex("Player");
            return asset.controlSchemes[m_PlayerSchemeIndex];
        }
    }
    public interface ITankActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnLookAt(InputAction.CallbackContext context);
    }
}

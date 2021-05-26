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
            ""name"": ""ObjPlayerTank"",
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
                    ""type"": ""Value"",
                    ""id"": ""5ca379eb-8d2e-4d3b-ade9-d0b867276830"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""ecdb7786-d57d-466f-8129-cf0f7b7f2a03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialShoot"",
                    ""type"": ""Button"",
                    ""id"": ""4c2dc192-30b0-46aa-a8cc-978a0f8f7de3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""ba3d42a3-e269-4bfc-94d3-51d5d719291b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""bb2c7432-201c-44cb-9b29-eeb8484621f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""67dd3360-6b2b-4939-b1fb-01ec50a4f4d2"",
                    ""expectedControlType"": ""Button"",
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
                    ""id"": ""2495ca43-b8d5-4345-a018-63179be09cfd"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08ea1fbb-e242-407a-a38f-c83ca6941a27"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02dd0554-e422-49e8-a855-28d0d70eaa82"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53923257-1ec4-4e60-9226-bb6b8178ffb5"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dffa1cdb-5672-42cb-bea6-e21043fa4838"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bfcb8c3-d5f3-49c9-ad08-be2d4bc64d0e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
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
        // ObjPlayerTank
        m_ObjPlayerTank = asset.FindActionMap("ObjPlayerTank", throwIfNotFound: true);
        m_ObjPlayerTank_Move = m_ObjPlayerTank.FindAction("Move", throwIfNotFound: true);
        m_ObjPlayerTank_Rotate = m_ObjPlayerTank.FindAction("Rotate", throwIfNotFound: true);
        m_ObjPlayerTank_Shoot = m_ObjPlayerTank.FindAction("Shoot", throwIfNotFound: true);
        m_ObjPlayerTank_LookAt = m_ObjPlayerTank.FindAction("LookAt", throwIfNotFound: true);
        m_ObjPlayerTank_Reload = m_ObjPlayerTank.FindAction("Reload", throwIfNotFound: true);
        m_ObjPlayerTank_SpecialShoot = m_ObjPlayerTank.FindAction("SpecialShoot", throwIfNotFound: true);
        m_ObjPlayerTank_Skill1 = m_ObjPlayerTank.FindAction("Skill1", throwIfNotFound: true);
        m_ObjPlayerTank_Skill2 = m_ObjPlayerTank.FindAction("Skill2", throwIfNotFound: true);
        m_ObjPlayerTank_Pause = m_ObjPlayerTank.FindAction("Pause", throwIfNotFound: true);
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

    // ObjPlayerTank
    private readonly InputActionMap m_ObjPlayerTank;
    private IObjPlayerTankActions m_ObjPlayerTankActionsCallbackInterface;
    private readonly InputAction m_ObjPlayerTank_Move;
    private readonly InputAction m_ObjPlayerTank_Rotate;
    private readonly InputAction m_ObjPlayerTank_Shoot;
    private readonly InputAction m_ObjPlayerTank_LookAt;
    private readonly InputAction m_ObjPlayerTank_Reload;
    private readonly InputAction m_ObjPlayerTank_SpecialShoot;
    private readonly InputAction m_ObjPlayerTank_Skill1;
    private readonly InputAction m_ObjPlayerTank_Skill2;
    private readonly InputAction m_ObjPlayerTank_Pause;
    public struct ObjPlayerTankActions
    {
        private @PlayerControls m_Wrapper;
        public ObjPlayerTankActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_ObjPlayerTank_Move;
        public InputAction @Rotate => m_Wrapper.m_ObjPlayerTank_Rotate;
        public InputAction @Shoot => m_Wrapper.m_ObjPlayerTank_Shoot;
        public InputAction @LookAt => m_Wrapper.m_ObjPlayerTank_LookAt;
        public InputAction @Reload => m_Wrapper.m_ObjPlayerTank_Reload;
        public InputAction @SpecialShoot => m_Wrapper.m_ObjPlayerTank_SpecialShoot;
        public InputAction @Skill1 => m_Wrapper.m_ObjPlayerTank_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_ObjPlayerTank_Skill2;
        public InputAction @Pause => m_Wrapper.m_ObjPlayerTank_Pause;
        public InputActionMap Get() { return m_Wrapper.m_ObjPlayerTank; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ObjPlayerTankActions set) { return set.Get(); }
        public void SetCallbacks(IObjPlayerTankActions instance)
        {
            if (m_Wrapper.m_ObjPlayerTankActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnRotate;
                @Shoot.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnShoot;
                @LookAt.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnLookAt;
                @LookAt.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnLookAt;
                @LookAt.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnLookAt;
                @Reload.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnReload;
                @SpecialShoot.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSpecialShoot;
                @SpecialShoot.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSpecialShoot;
                @SpecialShoot.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSpecialShoot;
                @Skill1.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSkill1;
                @Skill2.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnSkill2;
                @Pause.started -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_ObjPlayerTankActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_ObjPlayerTankActionsCallbackInterface = instance;
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
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @SpecialShoot.started += instance.OnSpecialShoot;
                @SpecialShoot.performed += instance.OnSpecialShoot;
                @SpecialShoot.canceled += instance.OnSpecialShoot;
                @Skill1.started += instance.OnSkill1;
                @Skill1.performed += instance.OnSkill1;
                @Skill1.canceled += instance.OnSkill1;
                @Skill2.started += instance.OnSkill2;
                @Skill2.performed += instance.OnSkill2;
                @Skill2.canceled += instance.OnSkill2;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public ObjPlayerTankActions @ObjPlayerTank => new ObjPlayerTankActions(this);
    private int m_PlayerSchemeIndex = -1;
    public InputControlScheme PlayerScheme
    {
        get
        {
            if (m_PlayerSchemeIndex == -1) m_PlayerSchemeIndex = asset.FindControlSchemeIndex("Player");
            return asset.controlSchemes[m_PlayerSchemeIndex];
        }
    }
    public interface IObjPlayerTankActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnLookAt(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnSpecialShoot(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}

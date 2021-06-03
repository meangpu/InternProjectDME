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
                }
            ]
        },
        {
            ""name"": ""Addons"",
            ""id"": ""2fd33026-92a7-4775-aaef-25d2741639a3"",
            ""actions"": [
                {
                    ""name"": ""AssignQ"",
                    ""type"": ""Button"",
                    ""id"": ""860f8bd0-b6af-4557-bdf6-3f948058fdbd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AssignE"",
                    ""type"": ""Button"",
                    ""id"": ""a52d46c9-1188-44cd-9cac-3edcb87e8fb6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""74972d76-747a-44da-891e-6be75416bf80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""850ce108-155d-49f1-b7bb-acf7c0907221"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AssignQ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f31dd4e-1eff-4ab5-9712-0f0691d95251"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AssignQ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""373ad0eb-320c-4e85-a6d5-ae7f3919e6dc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AssignE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e735eb3-7d29-4bad-8d31-5e3be9d8617f"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AssignE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6604779f-eac7-4324-a4d5-c455d6d709dc"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""AddonsMenu"",
            ""id"": ""256ea159-e6f9-4183-a271-027342ae3b9c"",
            ""actions"": [
                {
                    ""name"": ""ClearQ"",
                    ""type"": ""Button"",
                    ""id"": ""76be60fb-836a-4a26-a406-c64fc5e9ebff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClearE"",
                    ""type"": ""Button"",
                    ""id"": ""a76f0b9f-f536-4f52-af6e-1614520b17a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cbc2c95f-6d90-4b71-9f34-c5a20bd5b74d"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClearQ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f192352a-74d7-4130-a480-595a19427c2d"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClearE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""f0b1b426-f3c8-4cc8-b60c-d9863fdd9a8f"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""c562e75c-2f34-42c6-a78b-3e60c7ed89bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c0872a64-a265-4387-9ca6-c6d882bd1a0c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BuyMenu"",
            ""id"": ""7306b463-3410-45c7-8cf9-23d65880d8f6"",
            ""actions"": [
                {
                    ""name"": ""BuyMode"",
                    ""type"": ""Button"",
                    ""id"": ""f62552c9-fbad-43c4-a7f3-93f23f5a2a9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""2e5a9480-9b63-4d37-9daa-fc2bd04cb990"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2ad79bfa-9d88-4baf-8dd8-f5a5eb5aefb4"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BuyMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e2fe369-636f-4f56-842d-52713bb89c22"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
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
        m_Tank_Reload = m_Tank.FindAction("Reload", throwIfNotFound: true);
        m_Tank_SpecialShoot = m_Tank.FindAction("SpecialShoot", throwIfNotFound: true);
        m_Tank_Skill1 = m_Tank.FindAction("Skill1", throwIfNotFound: true);
        m_Tank_Skill2 = m_Tank.FindAction("Skill2", throwIfNotFound: true);
        // Addons
        m_Addons = asset.FindActionMap("Addons", throwIfNotFound: true);
        m_Addons_AssignQ = m_Addons.FindAction("AssignQ", throwIfNotFound: true);
        m_Addons_AssignE = m_Addons.FindAction("AssignE", throwIfNotFound: true);
        m_Addons_Cancel = m_Addons.FindAction("Cancel", throwIfNotFound: true);
        // AddonsMenu
        m_AddonsMenu = asset.FindActionMap("AddonsMenu", throwIfNotFound: true);
        m_AddonsMenu_ClearQ = m_AddonsMenu.FindAction("ClearQ", throwIfNotFound: true);
        m_AddonsMenu_ClearE = m_AddonsMenu.FindAction("ClearE", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Pause = m_Menu.FindAction("Pause", throwIfNotFound: true);
        // BuyMenu
        m_BuyMenu = asset.FindActionMap("BuyMenu", throwIfNotFound: true);
        m_BuyMenu_BuyMode = m_BuyMenu.FindAction("BuyMode", throwIfNotFound: true);
        m_BuyMenu_MousePosition = m_BuyMenu.FindAction("MousePosition", throwIfNotFound: true);
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
    private readonly InputAction m_Tank_Reload;
    private readonly InputAction m_Tank_SpecialShoot;
    private readonly InputAction m_Tank_Skill1;
    private readonly InputAction m_Tank_Skill2;
    public struct TankActions
    {
        private @PlayerControls m_Wrapper;
        public TankActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Tank_Move;
        public InputAction @Rotate => m_Wrapper.m_Tank_Rotate;
        public InputAction @Shoot => m_Wrapper.m_Tank_Shoot;
        public InputAction @LookAt => m_Wrapper.m_Tank_LookAt;
        public InputAction @Reload => m_Wrapper.m_Tank_Reload;
        public InputAction @SpecialShoot => m_Wrapper.m_Tank_SpecialShoot;
        public InputAction @Skill1 => m_Wrapper.m_Tank_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_Tank_Skill2;
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
                @Reload.started -= m_Wrapper.m_TankActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnReload;
                @SpecialShoot.started -= m_Wrapper.m_TankActionsCallbackInterface.OnSpecialShoot;
                @SpecialShoot.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnSpecialShoot;
                @SpecialShoot.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnSpecialShoot;
                @Skill1.started -= m_Wrapper.m_TankActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnSkill1;
                @Skill2.started -= m_Wrapper.m_TankActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnSkill2;
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
            }
        }
    }
    public TankActions @Tank => new TankActions(this);

    // Addons
    private readonly InputActionMap m_Addons;
    private IAddonsActions m_AddonsActionsCallbackInterface;
    private readonly InputAction m_Addons_AssignQ;
    private readonly InputAction m_Addons_AssignE;
    private readonly InputAction m_Addons_Cancel;
    public struct AddonsActions
    {
        private @PlayerControls m_Wrapper;
        public AddonsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AssignQ => m_Wrapper.m_Addons_AssignQ;
        public InputAction @AssignE => m_Wrapper.m_Addons_AssignE;
        public InputAction @Cancel => m_Wrapper.m_Addons_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Addons; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AddonsActions set) { return set.Get(); }
        public void SetCallbacks(IAddonsActions instance)
        {
            if (m_Wrapper.m_AddonsActionsCallbackInterface != null)
            {
                @AssignQ.started -= m_Wrapper.m_AddonsActionsCallbackInterface.OnAssignQ;
                @AssignQ.performed -= m_Wrapper.m_AddonsActionsCallbackInterface.OnAssignQ;
                @AssignQ.canceled -= m_Wrapper.m_AddonsActionsCallbackInterface.OnAssignQ;
                @AssignE.started -= m_Wrapper.m_AddonsActionsCallbackInterface.OnAssignE;
                @AssignE.performed -= m_Wrapper.m_AddonsActionsCallbackInterface.OnAssignE;
                @AssignE.canceled -= m_Wrapper.m_AddonsActionsCallbackInterface.OnAssignE;
                @Cancel.started -= m_Wrapper.m_AddonsActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_AddonsActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_AddonsActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_AddonsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AssignQ.started += instance.OnAssignQ;
                @AssignQ.performed += instance.OnAssignQ;
                @AssignQ.canceled += instance.OnAssignQ;
                @AssignE.started += instance.OnAssignE;
                @AssignE.performed += instance.OnAssignE;
                @AssignE.canceled += instance.OnAssignE;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public AddonsActions @Addons => new AddonsActions(this);

    // AddonsMenu
    private readonly InputActionMap m_AddonsMenu;
    private IAddonsMenuActions m_AddonsMenuActionsCallbackInterface;
    private readonly InputAction m_AddonsMenu_ClearQ;
    private readonly InputAction m_AddonsMenu_ClearE;
    public struct AddonsMenuActions
    {
        private @PlayerControls m_Wrapper;
        public AddonsMenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ClearQ => m_Wrapper.m_AddonsMenu_ClearQ;
        public InputAction @ClearE => m_Wrapper.m_AddonsMenu_ClearE;
        public InputActionMap Get() { return m_Wrapper.m_AddonsMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AddonsMenuActions set) { return set.Get(); }
        public void SetCallbacks(IAddonsMenuActions instance)
        {
            if (m_Wrapper.m_AddonsMenuActionsCallbackInterface != null)
            {
                @ClearQ.started -= m_Wrapper.m_AddonsMenuActionsCallbackInterface.OnClearQ;
                @ClearQ.performed -= m_Wrapper.m_AddonsMenuActionsCallbackInterface.OnClearQ;
                @ClearQ.canceled -= m_Wrapper.m_AddonsMenuActionsCallbackInterface.OnClearQ;
                @ClearE.started -= m_Wrapper.m_AddonsMenuActionsCallbackInterface.OnClearE;
                @ClearE.performed -= m_Wrapper.m_AddonsMenuActionsCallbackInterface.OnClearE;
                @ClearE.canceled -= m_Wrapper.m_AddonsMenuActionsCallbackInterface.OnClearE;
            }
            m_Wrapper.m_AddonsMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ClearQ.started += instance.OnClearQ;
                @ClearQ.performed += instance.OnClearQ;
                @ClearQ.canceled += instance.OnClearQ;
                @ClearE.started += instance.OnClearE;
                @ClearE.performed += instance.OnClearE;
                @ClearE.canceled += instance.OnClearE;
            }
        }
    }
    public AddonsMenuActions @AddonsMenu => new AddonsMenuActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Pause;
    public struct MenuActions
    {
        private @PlayerControls m_Wrapper;
        public MenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Menu_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // BuyMenu
    private readonly InputActionMap m_BuyMenu;
    private IBuyMenuActions m_BuyMenuActionsCallbackInterface;
    private readonly InputAction m_BuyMenu_BuyMode;
    private readonly InputAction m_BuyMenu_MousePosition;
    public struct BuyMenuActions
    {
        private @PlayerControls m_Wrapper;
        public BuyMenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @BuyMode => m_Wrapper.m_BuyMenu_BuyMode;
        public InputAction @MousePosition => m_Wrapper.m_BuyMenu_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_BuyMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuyMenuActions set) { return set.Get(); }
        public void SetCallbacks(IBuyMenuActions instance)
        {
            if (m_Wrapper.m_BuyMenuActionsCallbackInterface != null)
            {
                @BuyMode.started -= m_Wrapper.m_BuyMenuActionsCallbackInterface.OnBuyMode;
                @BuyMode.performed -= m_Wrapper.m_BuyMenuActionsCallbackInterface.OnBuyMode;
                @BuyMode.canceled -= m_Wrapper.m_BuyMenuActionsCallbackInterface.OnBuyMode;
                @MousePosition.started -= m_Wrapper.m_BuyMenuActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_BuyMenuActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_BuyMenuActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_BuyMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @BuyMode.started += instance.OnBuyMode;
                @BuyMode.performed += instance.OnBuyMode;
                @BuyMode.canceled += instance.OnBuyMode;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public BuyMenuActions @BuyMenu => new BuyMenuActions(this);
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
        void OnReload(InputAction.CallbackContext context);
        void OnSpecialShoot(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
    }
    public interface IAddonsActions
    {
        void OnAssignQ(InputAction.CallbackContext context);
        void OnAssignE(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IAddonsMenuActions
    {
        void OnClearQ(InputAction.CallbackContext context);
        void OnClearE(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IBuyMenuActions
    {
        void OnBuyMode(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}

// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""0eccf09e-9a18-4033-b5c0-2a8961270ca6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2db115b2-1e5a-4c5b-9551-d602471150c6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""7bbd9382-8c17-4e09-932b-517776907096"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""f4c59494-21a7-46c2-aa58-2aac09f629b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""4d8a66ab-f60c-407f-a6b0-8feded4088e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD Keys"",
                    ""id"": ""8336dbea-5fbc-41c5-bad2-3805b09d44c6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fe43c136-0b64-45a7-9024-10410e0d5c31"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ad7358c3-3541-424a-b648-8d68d2b91621"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""353e1996-1967-4b5a-9878-712163af798c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""016e7c6c-1de9-4c6d-a0ba-832a7fb953db"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""77953e92-146d-40d7-a752-4a84f0948e89"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(max=0.125)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2980dcf1-750c-462f-af24-76f90adb8f1a"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cafd8a29-dea5-48d9-a999-cbab36ff638f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6719ec88-c70a-40c5-934e-2d84e0bc6eff"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b26dd513-cd3e-442a-a555-980233de5b05"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9558ef2-f713-4076-b1a4-251633d14e30"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddbef466-3267-4339-82d4-c123fb0c4088"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""0a4d95d4-e3a0-4aba-8764-510eee4dcae8"",
            ""actions"": [
                {
                    ""name"": ""Navigation"",
                    ""type"": ""Value"",
                    ""id"": ""98137938-7ff3-4d6f-a44a-b28c7b453d56"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""527eb6ca-6843-47cf-880e-dfc5d60f08dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Close"",
                    ""type"": ""Button"",
                    ""id"": ""9849ba51-cf38-461f-90b2-6402d90eab79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""fbcf0184-5879-429a-bc5e-194c0e6964a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d2105ad0-fdab-4dca-85a4-cfce5296a9ef"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e8247be3-e5c9-4baf-a955-2dceeb3fc625"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""28bba390-89fb-4442-bd38-c66c5786863a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3f198533-2878-41fc-8afa-0591d50f3076"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2664dde9-48c9-4bfa-bea6-65b0f190873c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a7371b9a-65cd-4860-afe6-12858209410d"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(max=0.125)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f121875-48cb-497f-b800-6b9ca48e3b42"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06d0d7fe-eae8-42d6-b228-221bc23bf8cc"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8de7b972-470b-4773-b2a7-e1c05980d950"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b694a249-26aa-4c24-8454-37beaa52ffa9"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b35b6a2-c959-4e72-9e2f-3eba85936574"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6cfa733-d4cb-4eba-8faa-f10312727ea6"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ItemDetails"",
            ""id"": ""8ca2b94b-1986-4c5c-9d29-bd04500adfcb"",
            ""actions"": [
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""6d140727-e44e-4610-82cc-193edbb36662"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseInventory"",
                    ""type"": ""Button"",
                    ""id"": ""26a38797-dcc1-4fe8-b27a-0d021d2fd3c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""7b41ffe1-f4bf-478f-a800-ff242c5fdaca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2999bdfb-6182-4f26-953a-235e1a48ba02"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60dcab91-6cae-4de7-9961-70c7527e7e77"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8a015bf-f2c9-43cd-ae49-b6d4344be5a2"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abb263b1-d610-4e60-bbcd-f835177edb11"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac1b9cbc-cc9c-43d4-a237-05e345bd297b"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1c555fc-fd1b-4a5b-8342-480511098a38"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Chest"",
            ""id"": ""d44bcab9-9f71-4b34-b812-4ed1df869ea1"",
            ""actions"": [
                {
                    ""name"": ""Navigation"",
                    ""type"": ""Value"",
                    ""id"": ""9c252bd5-452f-443e-bf53-e5c6c488e92f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""fedacc18-d547-4764-849a-86087633186b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Close"",
                    ""type"": ""Button"",
                    ""id"": ""b61015a0-8df7-47a8-800f-bb8f63dec89b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""cbfbef14-3fb5-4d49-becd-fcdc86c4144f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""336eda93-da51-4a87-95ab-c375ef137a8f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aeb8cf67-6994-4bec-98a5-4d2491065284"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""506b8971-69fe-4b37-be43-d96379c0b5b6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7e76ebd0-4a6c-4bbd-ba9d-f986eeebc84a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""37d89e73-d548-4456-9204-25b260231d15"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""439f3d02-4228-41f8-9fe0-268acd3e6780"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0dec5747-7a17-4f76-b1e4-94fcbe9aab2a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c53f984-620c-4205-85fe-05eac7b6c367"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a8a3ed3-8a07-4bfa-937b-6895d646e07e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Interaction = m_Player.FindAction("Interaction", throwIfNotFound: true);
        m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_Navigation = m_Inventory.FindAction("Navigation", throwIfNotFound: true);
        m_Inventory_Select = m_Inventory.FindAction("Select", throwIfNotFound: true);
        m_Inventory_Close = m_Inventory.FindAction("Close", throwIfNotFound: true);
        m_Inventory_Back = m_Inventory.FindAction("Back", throwIfNotFound: true);
        // ItemDetails
        m_ItemDetails = asset.FindActionMap("ItemDetails", throwIfNotFound: true);
        m_ItemDetails_Use = m_ItemDetails.FindAction("Use", throwIfNotFound: true);
        m_ItemDetails_CloseInventory = m_ItemDetails.FindAction("CloseInventory", throwIfNotFound: true);
        m_ItemDetails_Back = m_ItemDetails.FindAction("Back", throwIfNotFound: true);
        // Chest
        m_Chest = asset.FindActionMap("Chest", throwIfNotFound: true);
        m_Chest_Navigation = m_Chest.FindAction("Navigation", throwIfNotFound: true);
        m_Chest_Select = m_Chest.FindAction("Select", throwIfNotFound: true);
        m_Chest_Close = m_Chest.FindAction("Close", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Interaction;
    private readonly InputAction m_Player_Inventory;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Interaction => m_Wrapper.m_Player_Interaction;
        public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Interaction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Inventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_Navigation;
    private readonly InputAction m_Inventory_Select;
    private readonly InputAction m_Inventory_Close;
    private readonly InputAction m_Inventory_Back;
    public struct InventoryActions
    {
        private @PlayerInputActions m_Wrapper;
        public InventoryActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigation => m_Wrapper.m_Inventory_Navigation;
        public InputAction @Select => m_Wrapper.m_Inventory_Select;
        public InputAction @Close => m_Wrapper.m_Inventory_Close;
        public InputAction @Back => m_Wrapper.m_Inventory_Back;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @Navigation.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNavigation;
                @Navigation.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNavigation;
                @Navigation.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNavigation;
                @Select.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelect;
                @Close.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnClose;
                @Close.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnClose;
                @Close.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnClose;
                @Back.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigation.started += instance.OnNavigation;
                @Navigation.performed += instance.OnNavigation;
                @Navigation.canceled += instance.OnNavigation;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Close.started += instance.OnClose;
                @Close.performed += instance.OnClose;
                @Close.canceled += instance.OnClose;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // ItemDetails
    private readonly InputActionMap m_ItemDetails;
    private IItemDetailsActions m_ItemDetailsActionsCallbackInterface;
    private readonly InputAction m_ItemDetails_Use;
    private readonly InputAction m_ItemDetails_CloseInventory;
    private readonly InputAction m_ItemDetails_Back;
    public struct ItemDetailsActions
    {
        private @PlayerInputActions m_Wrapper;
        public ItemDetailsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Use => m_Wrapper.m_ItemDetails_Use;
        public InputAction @CloseInventory => m_Wrapper.m_ItemDetails_CloseInventory;
        public InputAction @Back => m_Wrapper.m_ItemDetails_Back;
        public InputActionMap Get() { return m_Wrapper.m_ItemDetails; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ItemDetailsActions set) { return set.Get(); }
        public void SetCallbacks(IItemDetailsActions instance)
        {
            if (m_Wrapper.m_ItemDetailsActionsCallbackInterface != null)
            {
                @Use.started -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnUse;
                @CloseInventory.started -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnCloseInventory;
                @CloseInventory.performed -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnCloseInventory;
                @CloseInventory.canceled -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnCloseInventory;
                @Back.started -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_ItemDetailsActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_ItemDetailsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
                @CloseInventory.started += instance.OnCloseInventory;
                @CloseInventory.performed += instance.OnCloseInventory;
                @CloseInventory.canceled += instance.OnCloseInventory;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public ItemDetailsActions @ItemDetails => new ItemDetailsActions(this);

    // Chest
    private readonly InputActionMap m_Chest;
    private IChestActions m_ChestActionsCallbackInterface;
    private readonly InputAction m_Chest_Navigation;
    private readonly InputAction m_Chest_Select;
    private readonly InputAction m_Chest_Close;
    public struct ChestActions
    {
        private @PlayerInputActions m_Wrapper;
        public ChestActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigation => m_Wrapper.m_Chest_Navigation;
        public InputAction @Select => m_Wrapper.m_Chest_Select;
        public InputAction @Close => m_Wrapper.m_Chest_Close;
        public InputActionMap Get() { return m_Wrapper.m_Chest; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ChestActions set) { return set.Get(); }
        public void SetCallbacks(IChestActions instance)
        {
            if (m_Wrapper.m_ChestActionsCallbackInterface != null)
            {
                @Navigation.started -= m_Wrapper.m_ChestActionsCallbackInterface.OnNavigation;
                @Navigation.performed -= m_Wrapper.m_ChestActionsCallbackInterface.OnNavigation;
                @Navigation.canceled -= m_Wrapper.m_ChestActionsCallbackInterface.OnNavigation;
                @Select.started -= m_Wrapper.m_ChestActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_ChestActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_ChestActionsCallbackInterface.OnSelect;
                @Close.started -= m_Wrapper.m_ChestActionsCallbackInterface.OnClose;
                @Close.performed -= m_Wrapper.m_ChestActionsCallbackInterface.OnClose;
                @Close.canceled -= m_Wrapper.m_ChestActionsCallbackInterface.OnClose;
            }
            m_Wrapper.m_ChestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigation.started += instance.OnNavigation;
                @Navigation.performed += instance.OnNavigation;
                @Navigation.canceled += instance.OnNavigation;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Close.started += instance.OnClose;
                @Close.performed += instance.OnClose;
                @Close.canceled += instance.OnClose;
            }
        }
    }
    public ChestActions @Chest => new ChestActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnNavigation(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnClose(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
    public interface IItemDetailsActions
    {
        void OnUse(InputAction.CallbackContext context);
        void OnCloseInventory(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
    public interface IChestActions
    {
        void OnNavigation(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnClose(InputAction.CallbackContext context);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Player Input/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Grounded"",
            ""id"": ""842d2270-585b-457d-8f67-958c136da66b"",
            ""actions"": [
                {
                    ""name"": ""Walk"",
                    ""type"": ""Value"",
                    ""id"": ""9b3e1cdc-e77c-45f6-82a3-b5cbf1563d43"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AimUpdate"",
                    ""type"": ""Value"",
                    ""id"": ""82a6b332-5567-4ac1-8d61-965513aec78a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Value"",
                    ""id"": ""5181aa57-3e66-4dfb-9515-0871be9e610c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""0eb581d2-89ba-435f-b543-7987291c21e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AimDownSights"",
                    ""type"": ""Value"",
                    ""id"": ""2c095827-bf57-4a63-962e-cd6a6ddd3773"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""c4be260f-5ff3-47b5-be4e-2a0ca7fb06e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""22cf6394-d3fe-4c62-9d2e-2755a7a099e4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""845f4d9c-21aa-473d-b35d-d0cad0dda005"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0d8a406b-967c-4ab2-8dbd-6fbd5a75b5e4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1f490463-fdbd-4e07-a62b-40236ca72faa"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0f6da556-29de-404b-8a11-5dd21e06d135"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""9b1c6367-803c-4339-a808-1f4330ee92af"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""617d4bca-8e3e-476b-923d-e2fec667ee30"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""51d6f83c-0bed-4f4c-88dd-a82d77736775"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4b0c9e4a-f2f5-4c04-bdf7-b1635b52dcc2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c32009f3-15e5-45e0-86d5-124f21581d1d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""646ec018-d4e1-4e05-9a62-93eceefb38b6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""695d2432-09b9-4775-84c0-bad6809b3042"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""AimUpdate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b37c3f7b-fc03-4eac-9944-6b4c12f9264f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""AimUpdate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2001edc4-15cf-486d-b8c7-f8fff0d2aecb"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60958bd6-4104-4d62-91ef-afc7e8b45d85"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0eb022e6-ca40-4559-9400-87101a15528c"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47068b64-c37c-40fc-801f-15b9638c2b69"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02749782-6ded-44d0-970a-9092f9de303e"",
                    ""path"": ""<Mouse>/{SecondaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""AimDownSights"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a341e448-0975-402e-9c86-9bc37a9e5aac"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""b3eab6b1-c125-4517-bf37-a0f140444174"",
            ""actions"": [
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""39d98cfb-daa2-4262-bec8-a403d9c385d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ToggleCamRotation"",
                    ""type"": ""Value"",
                    ""id"": ""74907035-b1f4-4dc6-8f13-ef269b97550c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""43e71a23-6e7e-4a58-b861-051d60edb2c4"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""1d225f5d-d11c-411f-9729-492e88610111"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""8d28ee28-61aa-4520-bb5c-e214eba0e80d"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""a64e10ef-b19f-4957-b5a7-5ccc72e25647"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.015)"",
                    ""groups"": ""Controller"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""MouseRotate"",
                    ""id"": ""3488c9f5-2a93-41fa-b740-37a5d015511d"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCamRotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""3e18d339-e33d-4440-a015-b9c5662291b8"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""ToggleCamRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""2f430528-0124-42aa-ac08-c45193c72283"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""ToggleCamRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""M&K"",
            ""bindingGroup"": ""M&K"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
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
        // Grounded
        m_Grounded = asset.FindActionMap("Grounded", throwIfNotFound: true);
        m_Grounded_Walk = m_Grounded.FindAction("Walk", throwIfNotFound: true);
        m_Grounded_AimUpdate = m_Grounded.FindAction("AimUpdate", throwIfNotFound: true);
        m_Grounded_Sprint = m_Grounded.FindAction("Sprint", throwIfNotFound: true);
        m_Grounded_Crouch = m_Grounded.FindAction("Crouch", throwIfNotFound: true);
        m_Grounded_AimDownSights = m_Grounded.FindAction("AimDownSights", throwIfNotFound: true);
        m_Grounded_PrimaryFire = m_Grounded.FindAction("PrimaryFire", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Zoom = m_Menu.FindAction("Zoom", throwIfNotFound: true);
        m_Menu_ToggleCamRotation = m_Menu.FindAction("ToggleCamRotation", throwIfNotFound: true);
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

    // Grounded
    private readonly InputActionMap m_Grounded;
    private List<IGroundedActions> m_GroundedActionsCallbackInterfaces = new List<IGroundedActions>();
    private readonly InputAction m_Grounded_Walk;
    private readonly InputAction m_Grounded_AimUpdate;
    private readonly InputAction m_Grounded_Sprint;
    private readonly InputAction m_Grounded_Crouch;
    private readonly InputAction m_Grounded_AimDownSights;
    private readonly InputAction m_Grounded_PrimaryFire;
    public struct GroundedActions
    {
        private @PlayerControls m_Wrapper;
        public GroundedActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Walk => m_Wrapper.m_Grounded_Walk;
        public InputAction @AimUpdate => m_Wrapper.m_Grounded_AimUpdate;
        public InputAction @Sprint => m_Wrapper.m_Grounded_Sprint;
        public InputAction @Crouch => m_Wrapper.m_Grounded_Crouch;
        public InputAction @AimDownSights => m_Wrapper.m_Grounded_AimDownSights;
        public InputAction @PrimaryFire => m_Wrapper.m_Grounded_PrimaryFire;
        public InputActionMap Get() { return m_Wrapper.m_Grounded; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GroundedActions set) { return set.Get(); }
        public void AddCallbacks(IGroundedActions instance)
        {
            if (instance == null || m_Wrapper.m_GroundedActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GroundedActionsCallbackInterfaces.Add(instance);
            @Walk.started += instance.OnWalk;
            @Walk.performed += instance.OnWalk;
            @Walk.canceled += instance.OnWalk;
            @AimUpdate.started += instance.OnAimUpdate;
            @AimUpdate.performed += instance.OnAimUpdate;
            @AimUpdate.canceled += instance.OnAimUpdate;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @AimDownSights.started += instance.OnAimDownSights;
            @AimDownSights.performed += instance.OnAimDownSights;
            @AimDownSights.canceled += instance.OnAimDownSights;
            @PrimaryFire.started += instance.OnPrimaryFire;
            @PrimaryFire.performed += instance.OnPrimaryFire;
            @PrimaryFire.canceled += instance.OnPrimaryFire;
        }

        private void UnregisterCallbacks(IGroundedActions instance)
        {
            @Walk.started -= instance.OnWalk;
            @Walk.performed -= instance.OnWalk;
            @Walk.canceled -= instance.OnWalk;
            @AimUpdate.started -= instance.OnAimUpdate;
            @AimUpdate.performed -= instance.OnAimUpdate;
            @AimUpdate.canceled -= instance.OnAimUpdate;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @AimDownSights.started -= instance.OnAimDownSights;
            @AimDownSights.performed -= instance.OnAimDownSights;
            @AimDownSights.canceled -= instance.OnAimDownSights;
            @PrimaryFire.started -= instance.OnPrimaryFire;
            @PrimaryFire.performed -= instance.OnPrimaryFire;
            @PrimaryFire.canceled -= instance.OnPrimaryFire;
        }

        public void RemoveCallbacks(IGroundedActions instance)
        {
            if (m_Wrapper.m_GroundedActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGroundedActions instance)
        {
            foreach (var item in m_Wrapper.m_GroundedActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GroundedActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GroundedActions @Grounded => new GroundedActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private List<IMenuActions> m_MenuActionsCallbackInterfaces = new List<IMenuActions>();
    private readonly InputAction m_Menu_Zoom;
    private readonly InputAction m_Menu_ToggleCamRotation;
    public struct MenuActions
    {
        private @PlayerControls m_Wrapper;
        public MenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Zoom => m_Wrapper.m_Menu_Zoom;
        public InputAction @ToggleCamRotation => m_Wrapper.m_Menu_ToggleCamRotation;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void AddCallbacks(IMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MenuActionsCallbackInterfaces.Add(instance);
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @ToggleCamRotation.started += instance.OnToggleCamRotation;
            @ToggleCamRotation.performed += instance.OnToggleCamRotation;
            @ToggleCamRotation.canceled += instance.OnToggleCamRotation;
        }

        private void UnregisterCallbacks(IMenuActions instance)
        {
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @ToggleCamRotation.started -= instance.OnToggleCamRotation;
            @ToggleCamRotation.performed -= instance.OnToggleCamRotation;
            @ToggleCamRotation.canceled -= instance.OnToggleCamRotation;
        }

        public void RemoveCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_MKSchemeIndex = -1;
    public InputControlScheme MKScheme
    {
        get
        {
            if (m_MKSchemeIndex == -1) m_MKSchemeIndex = asset.FindControlSchemeIndex("M&K");
            return asset.controlSchemes[m_MKSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IGroundedActions
    {
        void OnWalk(InputAction.CallbackContext context);
        void OnAimUpdate(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnAimDownSights(InputAction.CallbackContext context);
        void OnPrimaryFire(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnZoom(InputAction.CallbackContext context);
        void OnToggleCamRotation(InputAction.CallbackContext context);
    }
}
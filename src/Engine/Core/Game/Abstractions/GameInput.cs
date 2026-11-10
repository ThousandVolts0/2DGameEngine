using Engine.Mathematics;
using ImGuiNET;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Silk.NET.Input;
using System.Net;

namespace Engine.Core.Game.Abstractions
{
    public class GameInput : IDisposable
    {
        public event Action<Key>? OnKeyDown;
        public event Action<Key>? OnKeyUp;

        public event Action<MouseButton>? OnMouseDown;
        public event Action<MouseButton>? OnMouseUp;
        public event Action<Vector2>? OnMouseMove;

        public bool IsKeyPressed(Key key) => _activeKeyboard?.IsKeyPressed(key) ?? false;
        public bool IsMouseButtonDown(MouseButton button) => _activeMouse?.IsButtonPressed(button) ?? false;

        public Vector2 MousePosition => _activeMouse != null ? (Vector2)_activeMouse.Position : Vector2.Zero;

        private readonly IInputContext _context;
        private readonly ILogger<GameInput> _logger;
        private IMouse? _activeMouse = null;
        private IKeyboard? _activeKeyboard = null;

        internal GameInput(IInputContext context, ILogger<GameInput> logger)
        {
            _context = context;
            _logger = logger;

            SetActiveKeyboard(_context.Keyboards.FirstOrDefault());
            SetActiveMouse(_context.Mice.FirstOrDefault());
            _context.ConnectionChanged += HandleDeviceConnectionChanged;

            if (_activeKeyboard == null)
                _logger.LogWarning("No keyboard device connected on startup");

            if (_activeMouse == null)
                _logger.LogWarning("No mouse device connected on startup");
        }

        private void HandleDeviceConnectionChanged(IInputDevice device, bool status)
        {
            if (device == _activeKeyboard && !status) // Disable current keyboard and fallback to already connected device or none
            {
                SetActiveKeyboard(_context.Keyboards.FirstOrDefault(x => x != _activeKeyboard));
                return;
            }

            if (device == _activeMouse && !status) // Disable current keyboard and fallback to already connected device or none
            {
                SetActiveMouse(_context.Mice.FirstOrDefault(x => x != _activeMouse));
                return;
            }

            if (device is IKeyboard keyboard && _activeKeyboard == null) // Connect new keyboard, if there isn't already one active
            {
                SetActiveKeyboard(keyboard);
                return;
            }

            if (device is IMouse mouse && _activeMouse == null)  // Connect new mouse, if there isn't already one active
            {
                SetActiveMouse(mouse);
                return;
            }
        }

        private void SetActiveKeyboard(IKeyboard? keyboard)
        {
            if (_activeKeyboard != null) // Unbind previous events
            {
                _activeKeyboard.KeyDown -= HandleKeyDown;
                _activeKeyboard.KeyUp -= HandleKeyUp;
            }

            _activeKeyboard = keyboard;

            if (_activeKeyboard != null) // Bind new events
            {
                _activeKeyboard.KeyDown += HandleKeyDown;
                _activeKeyboard.KeyUp += HandleKeyUp;
            }
        }

        private void SetActiveMouse(IMouse? mouse)
        {
            if (_activeMouse != null) // Unbind previous events
            {
                _activeMouse.MouseDown -= HandleMouseDown;
                _activeMouse.MouseUp -= HandleMouseUp;
                _activeMouse.MouseMove -= HandleMouseMove;
            }

            _activeMouse = mouse;

            if (_activeMouse != null) // Bind new events
            {
                _activeMouse.MouseDown += HandleMouseDown;
                _activeMouse.MouseUp += HandleMouseUp;
                _activeMouse.MouseMove += HandleMouseMove;
            }
        }

        private void HandleMouseDown(IMouse device, MouseButton button) => OnMouseDown?.Invoke(button);
        private void HandleMouseMove(IMouse device, System.Numerics.Vector2 pos) => OnMouseMove?.Invoke((Vector2)pos);
        private void HandleMouseUp(IMouse device, MouseButton button) => OnMouseUp?.Invoke(button);
        private void HandleKeyDown(IKeyboard device, Key key, int scanCode) => OnKeyDown?.Invoke(key);
        private void HandleKeyUp(IKeyboard device, Key key, int scanCode) => OnKeyUp?.Invoke(key);

        public void Dispose()
        {
            _context.ConnectionChanged -= HandleDeviceConnectionChanged;
            SetActiveKeyboard(null);
            SetActiveMouse(null);
        }
    }
}

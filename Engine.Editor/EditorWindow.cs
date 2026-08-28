using ImGuiNET;
using Microsoft.Extensions.Logging;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using System.Drawing;

namespace Engine.Editor
{
    public class EditorWindow
    {
        private IWindow _window;
        private ILogger<EditorWindow> _logger;
        private ImGuiController _controller;
        private IInputContext _input;
        private GL _gl;

        public EditorWindow(IWindow window, ILogger<EditorWindow> logger)
        {
            _window = window;
            _logger = logger;
        }

        public void OnLoad()
        {
            _gl = _window.CreateOpenGL();
            _input = _window.CreateInput();
            _controller = new ImGuiController(_gl, _window, _input);

            _logger.LogInformation("Editor loaded");
        }

        public void OnUpdate(double deltaTime)
        {

        }

        public void OnRender(double deltaTime)
        {
            float dt = (float)deltaTime;

            _controller.Update(dt);

            _gl.ClearColor(Color.FromArgb(255, (int)(.45f * 255), (int)(.55f * 255), (int)(.60f * 255)));
            _gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            ImGui.ShowDemoWindow();
            _controller.Render();
        }

        public void OnClosing()
        {
            _controller.Dispose();
            _input.Dispose();
            _gl.Dispose();
        }

        public void OnResize(Vector2D<int> size)
        {
            _gl.Viewport(size);
        }
    }
}

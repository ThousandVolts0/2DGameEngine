using Engine.Components;
using Engine.Rendering;
using Engine.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;

namespace Runtime
{
    public class GameWindow
    {
        private IWindow _window;
        private GL _gl;

        private RenderPipeline _renderPipeline;
        private uint _program;

        private Viewport _viewport;
        private Camera _mainCam;
        private Scene _scene;

        public GameWindow(IWindow window)
        {
            _window = window;
        }

        public void OnLoad()
        {
            _gl = _window.CreateOpenGL();
            _program = _gl.CreateProgram();
            _renderPipeline = new RenderPipeline(_gl, _program);
            _scene = new Scene();
            _mainCam = new Camera();

            var mainCamEntity = new Entity("MainCamera", _mainCam);
            var squareEntity = new Entity("Square", new Renderer() { Object = new RenderObject(){ Data = RenderObjectData.Circle } });
            _scene.AddEntity(mainCamEntity);
            _scene.AddEntity(squareEntity);

            _viewport = new Viewport()
            {
                X = 0,
                Y = 0,
                Width = _window.Size.X,
                Height = _window.Size.Y
            };

            _gl.Viewport(_viewport.X, _viewport.Y, (uint)_viewport.Width, (uint)_viewport.Height);
            _gl.ClearColor(Color.CornflowerBlue);
        }

        public void OnUpdate(double deltaTime)
        {

        }

        public void OnRender(double deltaTime)
        {
            _gl.Clear(ClearBufferMask.ColorBufferBit);
            _renderPipeline.RenderScene(_scene, _viewport, _mainCam);
        }

        public void OnClosing()
        {
            _gl.Dispose();
        }

        public void OnResize(Vector2D<int> size)
        {
            _viewport.Width = size.X;
            _viewport.Height = size.Y;

            _gl.Viewport(_viewport.X, _viewport.Y, (uint)size.X, (uint)size.Y);
        }
    }
}

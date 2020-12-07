using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace _3d.UI {
    class UIButton : UIComponent {

        Shader shader;
        public UIButton(string vertexShaderPath, string fragmentShaderPath, Vector2 position, Vector2 offset) {
            shader = new Shader(vertexShaderPath, fragmentShaderPath);
        }

        public override void Render() {
            shader.UseShader();
            base.Render();
        }
    }
}
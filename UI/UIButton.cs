using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace _3d.UI {
    class UIButton : UIComponent {

        Vector2 dimensions;
        float[] vertices;
    
        Shader shader;
        int vao, vbo;

        public UIButton(string vertexShaderPath, string fragmentShaderPath, Vector2 position, Vector2 offset, Vector2 dimensions) {
            shader = new Shader(vertexShaderPath, fragmentShaderPath);
            this.dimensions = dimensions;

            float[] tempVertices = {
                position.X+((dimensions.X/2)+offset.X), position.Y+((dimensions.Y/2)+offset.Y), 0.0f,
                position.X+((dimensions.X/2)+offset.X), position.Y-((dimensions.Y/2)-offset.Y), 0.0f,
                position.X-((dimensions.X/2)+offset.X), position.Y-((dimensions.Y/2)-offset.Y), 0.0f,

                position.X-((dimensions.X/2)+offset.X), position.Y-((dimensions.Y/2)-offset.Y), 0.0f,
                position.X-((dimensions.X/2)+offset.X), position.Y+((dimensions.Y/2)+offset.Y), 0.0f,
                position.X+((dimensions.X/2)+offset.X), position.Y+((dimensions.Y/2)+offset.Y), 0.0f,
            };

            vertices = tempVertices;

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float, false, 0, 0);
        }

        public override void Render() {
            base.Render();
            shader.UseShader();
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
        }
    }
}
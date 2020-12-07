using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System;
using _3d.UI.Events;

namespace _3d.UI {

    ///<summary>
    /// class that can initialize and render a User Interface on the screen
    ///</summary>
    class UserInterface {

        static Vector2 position = new Vector2(0, 0);
        static float uiSizeX = .4f;
        static Vector2 dimensions;

        public enum PositionType {Left = -1, Right = 1}
        List<UIComponent> userInterfaceComponents = new List<UIComponent>();

        Shader shader;
        int vao, vbo;

        float[] vertices;

        ///<summary>
        /// class that can initialize and render a User Interface on the screen
        ///</summary>
        public UserInterface(float width, float height, PositionType positionType, Camera camera) {
            shader = new Shader("Shaders/UI/vertexUIShader.glsl","Shaders/UI/fragmentUIShader.glsl");

            dimensions = new Vector2(position.X+(1-width),height);

            float[] tempVertices = {
                (float)positionType,               dimensions.Y,  0.0f,
                (float)positionType,              -dimensions.Y,  0.0f,
                (float)positionType*dimensions.X, -dimensions.Y,  0.0f,

                (float)positionType,               dimensions.Y,  0.0f,
                (float)positionType*dimensions.X,  dimensions.Y,  0.0f,
                (float)positionType*dimensions.X, -dimensions.Y,  0.0f
            };
            vertices = tempVertices;
            uiSizeX = width;

            position = new Vector2((((float)positionType*dimensions.X)+(float)positionType)/2,0);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float, false, 0, 0);

            AddUIComponent(new UIButton("Shaders/UI/UIButton/buttonShader.glsl",
                                        "Shaders/UI/UIButton/buttonFrag.glsl", 
                                        position, new Vector2(0,.3f), new Vector2(.3f, .1f), 
                                        camera, ButtonClickEvents.Action1));
        }

        public void AddUIComponent(UIComponent component) {
            userInterfaceComponents.Add(component);
        }

        public void Render() {

            try {
                foreach (UIComponent component in userInterfaceComponents) {
                    component.Render();
                }
            }   
            catch (Exception e) {}

            shader.UseShader();
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
        }
    }
}
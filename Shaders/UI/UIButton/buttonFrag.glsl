#version 400

out vec4 frag_Color;
uniform vec3 color = vec3(0.0,1,0.55);

void main() {
    frag_Color = vec4(color,1.0);
}

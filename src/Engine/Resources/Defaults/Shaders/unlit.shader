#type vertex

#version 330 core

layout (location = 0) in vec3 aPosition;
uniform mat4 projection;

void main()
{
    gl_Position = projection * vec4(aPosition, 1.0);
}

#type fragment

#version 330 core

out vec4 out_color;

void main()
{
    out_color = vec4(1.0, 0.5, 0.2, 1.0);
}
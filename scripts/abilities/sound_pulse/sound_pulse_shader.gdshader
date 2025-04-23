shader_type canvas_item;

uniform float radius : hint_range(0.0, 250.0);
uniform float width = 10.0;
uniform vec4 wave_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);

void fragment() {
    vec2 center = vec2(0.5);
    float dist = length(UV - center) * 512.0;

    float alpha = smoothstep(radius - width, radius, dist) * (1.0 - smoothstep(radius, radius + width, dist));

    COLOR = wave_color * alpha;
}
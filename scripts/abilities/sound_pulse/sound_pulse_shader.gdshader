shader_type canvas_item;

uniform float radius : hint_range(0.0, 250.0);
uniform float width = 10.0;
uniform vec4 wave_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);

uniform float wave_frequency : hint_range(0.0, 50.0) = 10.0;
uniform float wave_amplitude : hint_range(0.0, 20.0) = 5.0;
uniform float wave_speed : hint_range(0.0, 10.0) = 2.0;

void fragment() {
    vec2 center = vec2(0.5);
    vec2 delta = UV - center;
    float dist = length(delta) * 512.0;

    // Angle from center (polar coordinate)
    float angle = atan(delta.y, delta.x);

    // Add wavy distortion to the radius using sin(angle ± time)
    float wavy_radius = radius + sin(angle * wave_frequency + TIME * wave_speed) * wave_amplitude;

    float alpha = smoothstep(wavy_radius - width, wavy_radius, dist) * 
                  (1.0 - smoothstep(wavy_radius, wavy_radius + width, dist));

    COLOR = wave_color * alpha;
}
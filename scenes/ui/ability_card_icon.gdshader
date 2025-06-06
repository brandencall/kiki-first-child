shader_type canvas_item;

uniform vec4 shine_color = vec4(1.0);
uniform float shine_progress : hint_range(0.0, 1.0, 0.01) = 0.0;
uniform float shine_size : hint_range(0.01, 1.0, 0.01) = 0.1;
uniform float shine_angle : hint_range(0.0, 89.9, 0.1) = 45.0;
uniform float shine_strength : hint_range(0.0, 1.0, 0.01) = 0.5; // New!

float scale(float value, float inMin, float inMax, float outMin, float outMax) {
    return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
}

void fragment() {
    COLOR = texture(TEXTURE, UV);

    float slope = tan(radians(shine_angle));
    float progress = scale(shine_progress, 0.0, 1.0, -1.0 - shine_size - shine_size * slope, 1.0 * slope);

    float line = slope * UV.x - UV.y;

    float shine = step(line, progress + shine_size + shine_size * slope) - step(line, progress);

    // Apply transparency control with shine_strength
    COLOR.rgb = mix(COLOR.rgb, shine_color.rgb, shine * shine_color.a * shine_strength);
}
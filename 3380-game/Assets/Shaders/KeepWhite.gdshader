shader_type canvas_item;


void vertex() {
	// Called for every vertex the material is visible on.
	
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec4 color = texture(TEXTURE,UV);
	vec4 mods = COLOR / color;
	
	if (color.a == 1.0){
    if (color.rgb != vec3(1.0) && color.rgb != vec3(0.0)){
        	color *= mods;
    	}
	}
	COLOR = color;
}


//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}

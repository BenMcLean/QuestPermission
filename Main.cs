using Godot;
using System;

public class Main : Spatial
{
	private DosScreen DosScreen;

	public override void _Ready()
	{
		VisualServer.SetDefaultClearColor(Color.Color8(0, 0, 0, 255));

		Camera camera = new Camera()
		{
			Current = true,
		};
		//camera.SetScript(ResourceLoader.Load("res://maujoe.camera_control/camera_control.gd") as GDScript);
		AddChild(camera);

		AddChild(new WorldEnvironment()
		{
			Environment = new Godot.Environment()
			{
				BackgroundColor = Color.Color8(85, 85, 85, 255),
				BackgroundMode = Godot.Environment.BGMode.Color,
			},
		});

		AddChild(DosScreen = new DosScreen()
		{
			GlobalTransform = new Transform(Basis.Identity, new Vector3(0, 0, -2)),
		});

		DosScreen.Screen.WriteLine("Bad command or file name.");
	}

	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}

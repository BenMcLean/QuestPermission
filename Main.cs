using Godot;
using System;

public class Main : Spatial
{
	public string Path { get; set; } = "";
	public ARVRInterface ARVRInterface { get; set; }
	public ARVROrigin ARVROrigin { get; set; }
	public ARVRCamera ARVRCamera { get; set; }
	public ARVRController LeftController { get; set; }
	public ARVRController RightController { get; set; }

	public enum LoadingState
	{
		ASK_PERMISSION,
		DOWNLOAD_SHAREWARE
	};

	public LoadingState State { get; set; }

	private DosScreen DosScreen;

	public override void _Ready()
	{
		VisualServer.SetDefaultClearColor(Color.Color8(0, 0, 0, 255));

		//Camera camera = new Camera()
		//{
		//	Current = true,
		//};
		//camera.SetScript(ResourceLoader.Load("res://maujoe.camera_control/camera_control.gd") as GDScript);
		//AddChild(camera);

		AddChild(ARVROrigin = new ARVROrigin());
		ARVROrigin.AddChild(ARVRCamera = new ARVRCamera()
		{
			Current = true,
		});
		ARVROrigin.AddChild(LeftController = new ARVRController()
		{
			ControllerId = 1,
		});
		LeftController.AddChild(GD.Load<PackedScene>("res://OQ_Toolkit/OQ_ARVRController/models3d/OculusQuestTouchController_Left.gltf").Instance());
		ARVROrigin.AddChild(RightController = new ARVRController()
		{
			ControllerId = 2,
		});
		RightController.AddChild(GD.Load<PackedScene>("res://OQ_Toolkit/OQ_ARVRController/models3d/OculusQuestTouchController_Right.gltf").Instance());

		AddChild(new WorldEnvironment()
		{
			Environment = new Godot.Environment()
			{
				BackgroundColor = Color.Color8(0, 0, 0, 255),
				BackgroundMode = Godot.Environment.BGMode.Color,
			},
		});

		AddChild(DosScreen = new DosScreen()
		{
			GlobalTransform = new Transform(Basis.Identity, new Vector3(0, 0, -2)),
		});

		DosScreen.Screen.WriteLine("Platform detected: " + OS.GetName());

		switch (OS.GetName())
		{
			case "Android":
				Path = "/storage/emulated/0/";
				State = LoadingState.ASK_PERMISSION;
				ARVRInterface = ARVRServer.FindInterface("OVRMobile");
				break;
			default:
				State = LoadingState.DOWNLOAD_SHAREWARE;
				ARVRInterface = ARVRServer.FindInterface("OpenVR");
				break;
		}

		if (ARVRInterface != null && ARVRInterface.Initialize())
			GetViewport().Arvr = true;

		DosScreen.Screen.WriteLine("Current loading state: " + State);
	}

	public override void _Process(float delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		DosScreen.Screen.WriteLine(
			"InputEvent: \"" + @event + "\""
			);
	}
}
